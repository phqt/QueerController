using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ClockRotation : MonoBehaviour
{
    public bool isTime;

    public float endRotationHour = 0;
    public float endRoatationMin = 0;
    public float hourRotationTime = 0;
    public float minRotationTime = 0;

    public GameObject bigHand;
    public GameObject smallHand;

    public float sceneSwitchDelay;

    void Start()
    {
        isTime = false;
    }

    void Update()
    {
        if (isTime == true)
        {
            smallHand.transform.DOLocalRotate(new Vector3(0, endRotationHour, 0), hourRotationTime).SetEase(Ease.InOutSine);
            bigHand.transform.DOLocalRotate(new Vector3(0, endRoatationMin, 0), minRotationTime).SetEase(Ease.InOutSine);
            StartCoroutine(nextScene());
        }
    }

    IEnumerator nextScene()
    {
        yield return new WaitForSeconds(sceneSwitchDelay);
        SceneManager.LoadScene(1);
    }
}
