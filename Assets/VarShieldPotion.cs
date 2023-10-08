using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VarShieldPotion : MonoBehaviour
{
    public void OnTriggerEnter2D (Collider2D hitInfo)
    {
        if(hitInfo.gameObject.layer == 8)
        {
            PlayerCombat instance = GameObject.Find("Player").GetComponent<PlayerCombat>();
            instance.UpdateShieldPotion(1);
            Destroy(gameObject);
        } 
    }
}
