using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamesRight : MonoBehaviour
{
    public float speed = 1f;
    public bool IsGrounded = false;
    public LayerMask groundLayers;

    public bool HasGivenDamage;
    public float DamageRange = 0.5f; 
    public Transform DamagePoint;
    public LayerMask playerLayers;

    void Start()
    {
        HasGivenDamage = false;
        Invoke("Stop", 5);
    }

    void Update()
    {
        transform.Translate(2 * Time.deltaTime * speed, 0,0);

        Collider2D[] Roll = Physics2D.OverlapBoxAll(DamagePoint.position, DamagePoint.localScale, 0, playerLayers);
        foreach(Collider2D player in Roll)
        {
            if(!HasGivenDamage)
            {
                PlayerCombat instance = GameObject.Find("Player").GetComponent<PlayerCombat>();
                if(instance.OrbIsActivated)
			    {
				    instance.TakeDamage(0);
			    } else if(!instance.CanTakeShieldPotion)
			    {
				    instance.TakeDamage(10);
                } else {
                    instance.TakeDamage(20);
                }
                HasGivenDamage = true;
                Invoke("ResetDamage", 2);  
            }
        }
    }

    void Stop()
    {
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        if (DamagePoint == null)
            return;

        Gizmos.DrawWireCube(DamagePoint.position, DamagePoint.localScale);
    }

    public void ResetDamage()
    {
        HasGivenDamage = false;
    }
}
