using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnableFIre : MonoBehaviour
{
    public ParticleSystem theFire;

    public bool isLit;

    public float sceneSwitchDelay;

    void Start()
    {
        isLit = false;
    }

    void Update()
    {
        if (isLit == false)
        {
            theFire.Pause();
        }

        if (isLit == true)
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
