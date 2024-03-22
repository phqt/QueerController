using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowSpawner : MonoBehaviour
{
    public float interval;
    public GameObject theSnow;

    void Start()
    {
        StartCoroutine(spawnSnow());
    }

    private IEnumerator spawnSnow()
    {
        yield return new WaitForSeconds(interval);
        GameObject snow = Instantiate(theSnow, transform.position, Quaternion.Euler(new Vector3(90, Random.Range(0,360))));
        StartCoroutine(spawnSnow());
    }
}
