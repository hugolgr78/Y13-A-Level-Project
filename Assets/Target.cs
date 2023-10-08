using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject attack2;
    void Start()
    {
        Invoke("Destroy", 1);
    }

    public void Destroy()
    {
        Destroy(gameObject);
        Instantiate(attack2, transform.position, transform.rotation);
    }
}
