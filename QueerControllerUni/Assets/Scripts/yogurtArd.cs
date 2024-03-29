using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.SceneManagement;

public class yogurtArd : MonoBehaviour
{
    public SerialController serialController;
    //public SpawnYogurt spawnYogurt;

    public GameObject yogurtOne;
    public GameObject yogurtTwo;
    public GameObject yogurtThree;
    public GameObject yogurtFour;

    public bool yogurtOneBool;
    public bool yogurtTwoBool;
    public bool yogurtThreeBool;
    public bool yogurtFourBool;

    public float sceneSwitchDelay;

    //private void Start()
    //{
    //    Application.logMessageReceived += OnMessageArrived;
    //}
    //void Update()
    //{
    //    OnMessageArrived(theNumber);
    //}
    private void Start()
    {

    }
    public void OnMessageArrived(string message)
    {
        int intValue;

        if (int.TryParse(message, out intValue))
        {

            yogurtOneBool = (intValue == 1);
            Debug.Log("Received value from Arduino: " + intValue);
            if (yogurtOneBool)
            {
                yogurtOne.SetActive(true);
            }

        }

        if (int.TryParse(message, out intValue))
        {

            yogurtTwoBool = (intValue == 2);
            Debug.Log("Received value from Arduino: " + intValue);
            if (yogurtTwoBool)
            {
                yogurtTwo.SetActive(true);
            }

        }

        if (int.TryParse(message, out intValue))
        {

            yogurtThreeBool = (intValue == 3);
            Debug.Log("Received value from Arduino: " + intValue);
            if (yogurtThreeBool)
            {
                yogurtThree.SetActive(true);
            }

        }

        if (int.TryParse(message, out intValue))
        {

            yogurtFourBool = (intValue == 4);
            Debug.Log("Received value from Arduino: " + intValue);
            if (yogurtFourBool)
            {
                yogurtFour.SetActive(true);
            }

        }

        if (yogurtOne.activeSelf & yogurtTwo.activeSelf & yogurtThree.activeSelf & yogurtFour.activeSelf)
        {
            StartCoroutine(nextScene());
        }

        //if (yogurtOneBool == true & yogurtTwoBool == true & yogurtThreeBool == true & yogurtFourBool == true)
        //{
        //    StartCoroutine(nextScene());
        //}

        IEnumerator nextScene()
        {
            yield return new WaitForSeconds(sceneSwitchDelay);
            SceneManager.LoadScene(2);
        }
        //if (stat == "2")
        //{
        //    Debug.Log("two");
        //}
        //if (stat == "3")
        //{
        //    Debug.Log("three");
        //}
        //if (stat == "4")
        //{
        //    Debug.Log("four");
        //}
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(2);
        }
    }
    void OnConnectionEvent(bool success)
    {
        Debug.Log(success ? "Device connected" : "Device disconnected");
    }
}
