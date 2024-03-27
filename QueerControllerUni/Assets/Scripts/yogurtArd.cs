using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class yogurtArd : MonoBehaviour
{
    public SerialController serialController;
    public SpawnYogurt spawnYogurt;
    public GameObject yogurtOne;

    void OnMessageArrived(string stat)
    {
        Debug.Log(stat);

        if (stat == "pressureSensor1")
        {
            Debug.Log("one");
            //spawnYogurt.firstYogurt = true;
            yogurtOne.SetActive(true);
        }
        if (stat == "2")
        {
            Debug.Log("two");
        }
        if (stat == "3")
        {
            Debug.Log("three");
        }
        if (stat == "4")
        {
            Debug.Log("four");
        }
    }

    void OnConnectionEvent(bool success)
    {
        Debug.Log(success ? "Device connected" : "Device disconnected");
    }
}
