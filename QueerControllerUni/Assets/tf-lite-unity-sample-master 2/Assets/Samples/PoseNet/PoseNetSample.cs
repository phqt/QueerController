using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using TensorFlowLite;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;

[RequireComponent(typeof(WebCamInput))]
[System.Obsolete("Use MoveNet instead")]
public class PoseNetSample : MonoBehaviour
{
    [SerializeField, FilePopup("*.tflite")]
    private string fileName = "posenet_mobilenet_v1_100_257x257_multi_kpt_stripped.tflite";

    [SerializeField]
    private RawImage cameraView = null;

    [SerializeField, Range(0f, 1f)]
    private float threshold = 0.5f;

    [SerializeField, Range(0f, 1f)]
    private float lineThickness = 0.5f;

    [SerializeField]
    private bool runBackground;

    private PoseNet poseNet;
    private readonly Vector3[] rtCorners = new Vector3[4];
    private PrimitiveDraw draw;
    private UniTask<bool> task;
    private PoseNet.Result[] results;
    private CancellationToken cancellationToken;

    // Fields for tracking movement
    private float danceTimer = 0f;
    private const float danceDuration = 10f; // Time window for tracking movement
    private PoseNet.Result[] prevResults = null;
    private float movementThreshold = 0.1f; // Threshold for detecting significant movement

    public GameObject thePenguin;
    public bool isDancing;
    Animator m_Animator;
    bool theDance;
    public float sceneSwitchDelay;
    public GameObject redoText;
    public TMP_Text dancePointsTxt;

    private void Start()
    {
        poseNet = new PoseNet(fileName);

        draw = new PrimitiveDraw(Camera.main, gameObject.layer)
        {
            color = Color.green,
        };

        cancellationToken = this.GetCancellationTokenOnDestroy();

        var webCamInput = GetComponent<WebCamInput>();
        webCamInput.OnTextureUpdate.AddListener(OnTextureUpdate);

        m_Animator = thePenguin.GetComponent<Animator>();
        theDance = false;
    }

    private void OnDestroy()
    {
        var webCamInput = GetComponent<WebCamInput>();
        webCamInput.OnTextureUpdate.RemoveListener(OnTextureUpdate);

        poseNet?.Dispose();
        draw?.Dispose();
    }

    private void Update()
    {
        DrawResult(results);

        if (isDancing == true)
        {
            Debug.Log("dancing");
            theDance = true;
            m_Animator.SetBool("toDance", true);
            StartCoroutine(nextScene());
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(4);
        }
    }

    private void OnTextureUpdate(Texture texture)
    {
        if (runBackground)
        {
            if (task.Status.IsCompleted())
            {
                task = InvokeAsync(texture);
            }
        }
        else
        {
            Invoke(texture);
        }
    }

    private void DrawResult(PoseNet.Result[] results)
    {
        if (results == null || results.Length == 0)
        {
            return;
        }

        var rect = cameraView.GetComponent<RectTransform>();
        rect.GetWorldCorners(rtCorners);
        float3 min = rtCorners[0];
        float3 max = rtCorners[2];

        var connections = PoseNet.Connections;
        int len = connections.GetLength(0);
        for (int i = 0; i < len; i++)
        {
            var a = results[(int)connections[i, 0]];
            var b = results[(int)connections[i, 1]];
            if (a.confidence >= threshold && b.confidence >= threshold)
            {
                draw.Line3D(
                    math.lerp(min, max, new float3(a.x, 1f - a.y, 0)),
                    math.lerp(min, max, new float3(b.x, 1f - b.y, 0)),
                    lineThickness
                );
            }
        }
        draw.Apply();
        CheckForDancing(results); // Check movement after drawing results
    }

    private void Invoke(Texture texture)
    {
        poseNet.Invoke(texture);
        results = poseNet.GetResults();
        cameraView.material = poseNet.transformMat;
    }

    private async UniTask<bool> InvokeAsync(Texture texture)
    {
        results = await poseNet.InvokeAsync(texture, cancellationToken);
        cameraView.material = poseNet.transformMat;
        return true;
    }

    private float cumulativeMovementEnergy = 0f; // Add this as a class-level variable

    private bool loggingEnabled = true; // Control logging

    private void CheckForDancing(PoseNet.Result[] currentResults)
    {
        if (prevResults == null || currentResults == null || prevResults.Length != currentResults.Length)
        {
            prevResults = new PoseNet.Result[currentResults.Length];
            Array.Copy(currentResults, prevResults, currentResults.Length);
            cumulativeMovementEnergy = 0f; // Reset cumulative movement when resetting results
            return;
        }

        float movementEnergy = 0f;
        for (int i = 0; i < currentResults.Length; i++)
        {
            var current = currentResults[i];
            var prev = prevResults[i];
            float distance = Mathf.Sqrt(Mathf.Pow(current.x - prev.x, 2) + Mathf.Pow(current.y - prev.y, 2));
            movementEnergy += distance;
        }

        // Accumulate movement energy over time
        cumulativeMovementEnergy += movementEnergy;

        if (loggingEnabled && movementEnergy > 0.1) // Log if logging is enabled and movement is significant
        {
            //Debug.Log($"Significant Movement Detected: {movementEnergy}, Cumulative Movement: {cumulativeMovementEnergy}, Time: {danceTimer} seconds");
            dancePointsTxt.text = math.round(cumulativeMovementEnergy).ToString();
        }

        Array.Copy(currentResults, prevResults, currentResults.Length);

        if (movementEnergy > 0)
        {
            danceTimer += Time.deltaTime;
        }

        if (cumulativeMovementEnergy > 1000 && danceTimer >= 10f)
        {
            Debug.Log("You have danced!");
            isDancing = true;
            loggingEnabled = false; // Stop logging once the user has successfully danced
            danceTimer = 0f;
            cumulativeMovementEnergy = 0f;
        }
        else if (danceTimer >= 10f && cumulativeMovementEnergy <= 1000)
        {
            Debug.Log("Please retry, keep moving!");
            // Reset but allow retries
            danceTimer = 0f;
            cumulativeMovementEnergy = 0f;
            loggingEnabled = true; // Keep logging enabled for the retry
            redoText.SetActive(true);
            StartCoroutine(redoDance());
        }
    }

    IEnumerator nextScene()
    {
        yield return new WaitForSeconds(sceneSwitchDelay);
        SceneManager.LoadScene(4);
    }

    IEnumerator redoDance()
    {
        yield return new WaitForSeconds(3);
        //SceneManager.LoadScene(3);
        redoText.SetActive(false);
    }

}

