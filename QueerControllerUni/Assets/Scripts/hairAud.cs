using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO.Ports;

public class hairAud : MonoBehaviour
{
    public ParticleSystem theFire;

    public SerialController serialController;

    public bool isLit;

    public float sceneSwitchDelay;

    public bool hairBool;

    void Start()
    {
        theFire.Pause();
    }

    public void OnMessageArrived(string message)
    {
        int intValue;

        if (int.TryParse(message, out intValue))
        {

            hairBool = (intValue == 10);
            Debug.Log("Received value from Arduino: " + intValue);
            if (hairBool)
            {
                theFire.GetComponentInChildren<Light>().intensity = 1f;
                theFire.Play();
                StartCoroutine(nextScene());
            }

        }

        IEnumerator nextScene()
        {
            yield return new WaitForSeconds(sceneSwitchDelay);
            SceneManager.LoadScene(3);
        }
    }
    void OnConnectionEvent(bool success)
    {
        Debug.Log(success ? "Device connected" : "Device disconnected");
    }
}
