using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerExplosion : MonoBehaviour
{
    public bool hasBeenHit;
    int attackDamage = 50;

    void Start()
    {
        Invoke("Destroy", 1);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D (Collider2D hitInfo)
	{
        if (hitInfo.gameObject.name.Equals ("Player") && !hasBeenHit) {
			PlayerCombat instance = GameObject.Find("Player").GetComponent<PlayerCombat>();
			instance.TakeDamage(attackDamage);
            hasBeenHit = true;
		}
    }
}
