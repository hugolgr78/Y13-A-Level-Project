using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GameObject platform1;

    void Start()
    {
        platform1.SetActive(true);
    }

    public void Wait(GameObject platformObject)
    {
        GameObject platform = platformObject;
        StartCoroutine(ResetPlatform(platform));
    }

    IEnumerator ResetPlatform(GameObject platform)
    {
        yield return new WaitForSeconds(5);
        platform.SetActive(true);
    }
}
