using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public float moveSpeed = 7f;
    public GameObject impactEffect;
	public Rigidbody2D rb;
	public PlayerCombat target;
	public Vector2 moveDirection;

    public virtual void Start()
    {
		rb = GetComponent<Rigidbody2D> ();
		target = GameObject.FindObjectOfType<PlayerCombat>();
		moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
		rb.velocity = new Vector2 (moveDirection.x, moveDirection.y);
		Destroy(gameObject, 3f);
    }

    public virtual void OnTriggerEnter2D (Collider2D hitInfo)
	{
        if(hitInfo.gameObject.layer == 10){
			Instantiate(impactEffect, transform.position, transform.rotation);
			Destroy(gameObject);
		}
    }
}
