using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public Transform damagePoint;
    public LayerMask playerLayers; 
    public float damageRange = 0.5f;
    public bool PlayerHit;

    
    void Update()
    {
        Collider2D[] hitplayers = Physics2D.OverlapCircleAll(damagePoint.position, damageRange, playerLayers);
        foreach(Collider2D player in hitplayers)
        {
            if(!PlayerHit)
            {
                HitPlayer();
            }
        } 
    }
    
    void OnDrawGizmosSelected()
    {
        if (damagePoint == null)
            return;

        Gizmos.DrawWireSphere(damagePoint.position, damageRange);
    }

    public void HitPlayer()
    {
        PlayerCombat instance = GameObject.Find("Player").GetComponent<PlayerCombat>();
        instance.TakeDamage(5);
        PlayerHit = true;
        Invoke("ResetPlayerHit", 2);
    }

    public void ResetPlayerHit()
    {
        PlayerHit = false;
    }
}
