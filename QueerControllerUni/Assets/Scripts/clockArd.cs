using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class clockArd : MonoBehaviour
{
    public SerialController serialController;

    public bool clockBool;

    //public bool isTime;

    public float endRotationHour = 0;
    public float endRoatationMin = 0;
    public float hourRotationTime = 0;
    public float minRotationTime = 0;

    public GameObject bigHand;
    public GameObject smallHand;

    public float sceneSwitchDelay;

    void Start()
    {
        clockBool = false;
    }

    public void OnMessageArrived(string message)
    {
        int intValue;

        if (int.TryParse(message, out intValue))
        {

            clockBool = (intValue == 30);
            Debug.Log("Received value from Arduino: " + intValue);
            if (clockBool)
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
    void OnConnectionEvent(bool success)
    {
        Debug.Log(success ? "Device connected" : "Device disconnected");
    }
}
