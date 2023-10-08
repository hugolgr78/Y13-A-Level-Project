using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VarCoin : MonoBehaviour
{
    public void OnTriggerEnter2D (Collider2D hitInfo)
    {
        if(hitInfo.gameObject.layer == 8)
        {
            ScoreManager instance = GameObject.Find("Canvas").GetComponent<ScoreManager>();
            instance.AddCoin(10);
            Destroy(gameObject);
        } 
    }
}
