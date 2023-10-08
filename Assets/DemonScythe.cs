using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonScythe : Projectiles
{
    float acceleration = 0f;
	int scytheDamage = 20;

    void Update()
    {
        moveSpeed += acceleration;
        acceleration += 1;
    }

	public void OnTriggerEnter2D (Collider2D hitInfo)
	{
		base.OnTriggerEnter2D(hitInfo);
		if (hitInfo.gameObject.name.Equals ("Player")) {
			PlayerCombat instance = GameObject.Find("Player").GetComponent<PlayerCombat>();
			instance.TakeDamage(scytheDamage);
			Instantiate(impactEffect, transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}
}
