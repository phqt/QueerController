using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class danceControll : MonoBehaviour
{
    public GameObject thePenguin;
    public bool isDancing;
    Animator m_Animator;
    bool theDance;
    public float sceneSwitchDelay;

    void Start()
    {
        m_Animator = thePenguin.GetComponent<Animator>();
        theDance = false;
    }

    void Update()
    {
        if (isDancing == true)
        {
            Debug.Log("dancing");
            theDance = true;
            m_Animator.SetBool("toDance", true);
            StartCoroutine(nextScene());
        }
    }

    IEnumerator nextScene()
    {
        yield return new WaitForSeconds(sceneSwitchDelay);
        SceneManager.LoadScene(4);
    }
}
