using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnYogurt : MonoBehaviour
{
    //public GameObject[] objectsToActivate; 
    //private int currentIndex = 0;
    public float sceneSwitchDelay;

    public GameObject yogurtOne;
    public GameObject yogurtTwo;
    public GameObject yogurtThree;
    public GameObject yogurtFour;

    public bool firstYogurt;
    public bool secondYogurt;
    public bool thirdYogurt;
    public bool fourthYogurt;

    //public GameObject yogurdArdHolder;
    //public yogurtArd yogurtScript;

    void Update()
    {

        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    ActivateNextObject();
        //}

        if (firstYogurt == true)
        {
            yogurtOne.SetActive(true);
        }

        if (secondYogurt == true)
        {
            yogurtTwo.SetActive(true);
        }

        if (thirdYogurt == true)
        {
            yogurtThree.SetActive(true);
        }

        if (fourthYogurt == true)
        {
            yogurtFour.SetActive(true);
        }

        if (firstYogurt == true & secondYogurt == true & thirdYogurt ==true & fourthYogurt == true)
        {
            StartCoroutine(nextScene());
        }
    }

    //void ActivateNextObject()
    //{

    //    if (objectsToActivate != null && objectsToActivate.Length > 0)
    //    {

    //        if (currentIndex < objectsToActivate.Length)
    //        {
    //            objectsToActivate[currentIndex].SetActive(true);
    //            currentIndex++;
    //        }
    //        else
    //        {
    //            Debug.LogWarning("All objects in the array have been activated!");
    //            StartCoroutine(nextScene());
    //        }
    //    }
    //    else
    //    {
    //        Debug.LogError("objectsToActivate array is not assigned or empty!");
    //    }
    //}

    IEnumerator nextScene()
    {
        yield return new WaitForSeconds(sceneSwitchDelay);
        SceneManager.LoadScene(2);
    }
}
