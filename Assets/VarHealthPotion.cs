using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VarHealthPotion : MonoBehaviour
{
    public void OnTriggerEnter2D (Collider2D hitInfo)
    {
        if(hitInfo.gameObject.layer == 8)
        {
            PlayerCombat instance = GameObject.Find("Player").GetComponent<PlayerCombat>();
            instance.UpdateHealthPotion(1);
            Destroy(gameObject);
        } 
    }
}
