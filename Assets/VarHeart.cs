using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VarHeart : MonoBehaviour
{
    public LayerMask playerLayers;
    public Transform HeartPoint;

    public float HeartRange = 0.5f;

    static public int healthPlus = 20;
    
    public void OnTriggerEnter2D (Collider2D hitInfo)
    {
       if(hitInfo.gameObject.layer == 8)
        {
            PlayerCombat instance = GameObject.Find("Player").GetComponent<PlayerCombat>();
            if (instance.UpdateHealth(healthPlus)) 
            {
                Destroy(gameObject);
            }
        } 
    }

    void OnDrawGizmosSelected()
    {
        if (HeartPoint == null)
            return;

        Gizmos.DrawWireSphere(HeartPoint.position, HeartRange);
    }
}
