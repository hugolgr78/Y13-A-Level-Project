using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolutionBeastProjectile : Projectiles
{
	int beastProjectileDamage = 30;
	void OnTriggerEnter2D (Collider2D hitInfo)
	{
		base.OnTriggerEnter2D(hitInfo);
		if (hitInfo.gameObject.name.Equals ("Player")) {
			PlayerCombat instance = GameObject.Find("Player").GetComponent<PlayerCombat>();
			instance.TakeDamage(beastProjectileDamage);
			Instantiate(impactEffect, transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}
}
