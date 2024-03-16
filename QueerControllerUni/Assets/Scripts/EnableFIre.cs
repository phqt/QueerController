using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableFIre : MonoBehaviour
{
    public ParticleSystem theFire;

    public bool isLit;

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
        }
    }
}
