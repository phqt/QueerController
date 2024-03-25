using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnYogurt : MonoBehaviour
{
    public GameObject[] objectsToActivate; 
    private int currentIndex = 0;
    public float sceneSwitchDelay;

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
                StartCoroutine(nextScene());
            }
        }
        else
        {
            Debug.LogError("objectsToActivate array is not assigned or empty!");
        }
    }

    IEnumerator nextScene()
    {
        yield return new WaitForSeconds(sceneSwitchDelay);
        SceneManager.LoadScene(2);
    }
}
