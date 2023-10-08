using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectiles
{
	int bulletDamage = 15;
	public void OnTriggerEnter2D (Collider2D hitInfo)
	{
		base.OnTriggerEnter2D(hitInfo);
		if (hitInfo.gameObject.name.Equals ("Player")) {
			PlayerCombat instance = GameObject.Find("Player").GetComponent<PlayerCombat>();
			instance.TakeDamage(bulletDamage);
			Instantiate(impactEffect, transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}
}
