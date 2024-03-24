using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnYogurt : MonoBehaviour
{
    public GameObject[] objectsToActivate; 
    private int currentIndex = 0; 

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            ActivateNextObject();
        }
    }

    void ActivateNextObject()
    {

        if (objectsToActivate != null && objectsToActivate.Length > 0)
        {

            if (currentIndex < objectsToActivate.Length)
            {
                objectsToActivate[currentIndex].SetActive(true);
                currentIndex++;
            }
            else
            {
                Debug.LogWarning("All objects in the array have been activated!");
            }
        }
        else
        {
            Debug.LogError("objectsToActivate array is not assigned or empty!");
        }
    }
}
