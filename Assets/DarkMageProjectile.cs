using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkMageProjectile : Projectiles
{
	int projectileDamage = 10;
	public override void Start() 
	{
        StartCoroutine(ExecuteAfterTime(2));
	}

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        rb = GetComponent<Rigidbody2D> ();
		target = GameObject.FindObjectOfType<PlayerCombat>();
		moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
		rb.velocity = new Vector2 (moveDirection.x, moveDirection.y);
		Destroy(gameObject, 3f);
    }
	
	void OnTriggerEnter2D (Collider2D hitInfo)
	{
		base.OnTriggerEnter2D(hitInfo);
		if (hitInfo.gameObject.name.Equals ("Player")) {
			PlayerCombat instance = GameObject.Find("Player").GetComponent<PlayerCombat>();
			instance.TakeDamage(projectileDamage);
			Instantiate(impactEffect, transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}
}
