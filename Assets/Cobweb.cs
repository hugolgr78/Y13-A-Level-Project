using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cobweb : Projectiles
{
	int cobwebDamage = 5;
	void OnTriggerEnter2D (Collider2D hitInfo)
	{
		base.OnTriggerEnter2D(hitInfo);
		if (hitInfo.gameObject.name.Equals ("Player")) {
			PlayerCombat instance = GameObject.Find("Player").GetComponent<PlayerCombat>();
			instance.TakeDamage(cobwebDamage);
            instance.FreezePlayer();
			Instantiate(impactEffect, transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}
}
