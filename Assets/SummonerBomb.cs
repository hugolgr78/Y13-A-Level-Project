using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerBomb : Projectiles
{
	public GameObject Explosion;
	
	public override void Start () 
	{
        StartCoroutine(ExecuteAfterTime(1));
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

	public override void OnTriggerEnter2D (Collider2D hitInfo)
	{
		if(hitInfo.gameObject.layer == 8 || hitInfo.gameObject.layer == 10)
        {
			Instantiate(Explosion, transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}
}
