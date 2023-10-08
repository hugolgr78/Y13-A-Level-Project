using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerraBeam : MonoBehaviour
{
   	float moveSpeed = 7f;
	public GameObject impactEffect;
	Rigidbody2D rb;
	Enemy target;
	Vector2 moveDirection;
    bool FacingEnemy;
    PlayerCombat player;
    float[] beamScale;
    
	void Start () 
	{
		rb = GetComponent<Rigidbody2D> ();
        target = GameObject.FindObjectOfType<Enemy>();
        player = GameObject.FindObjectOfType<PlayerCombat>();

        beamScale = new float[2];
        beamScale[0] = gameObject.transform.localScale.x;
        beamScale[1] = gameObject.transform.localScale.y;

        CharacterController2D instance = GameObject.Find("Player").GetComponent<CharacterController2D>();
        if(target.transform.position.x > player.transform.position.x && instance.m_FacingRight)
        {
            FacingEnemy = true;
        } else if(target.transform.position.x < player.transform.position.x && !instance.m_FacingRight) {
            FacingEnemy = true;
        } else {
            FacingEnemy = false;
        }

        if(target != null && FacingEnemy) 
        {
            moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
            rb.velocity = new Vector2 (moveDirection.x, moveDirection.y);
            if (moveDirection != Vector2.zero) 
            {
                float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            Destroy(gameObject, 5f);
        } else {
            if (target.transform.position.x > player.transform.position.x && !instance.m_FacingRight)
            {
                rb.velocity = transform.right * -moveSpeed;
                transform.localScale = new Vector2 (-beamScale[0], beamScale[1]);
            } else {
                rb.velocity = transform.right * moveSpeed; 
                transform.localScale = new Vector2 (beamScale[0], beamScale[1]);
            }
            Destroy(gameObject, 3f);
        }
        
	}

	void OnTriggerEnter2D (Collider2D hitInfo)
	{
        if(hitInfo.gameObject.layer == 9)
        {
            hitInfo.GetComponent<Enemy>().TakeDamage(20);
			Instantiate(impactEffect, transform.position, transform.rotation);  
        } else if(hitInfo.gameObject.layer == 10){
			Instantiate(impactEffect, transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}
}
