using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandElementalAttack1 : Projectiles
{
    SandElemental elemental;
    public float[] attackScale;
    int attackDamage = 30;

	public void Start () 
	{
		base.Start();
		
        attackScale = new float[2];
        attackScale[0] = gameObject.transform.localScale.x;
        attackScale[1] = gameObject.transform.localScale.y;
        elemental = GameObject.FindObjectOfType<SandElemental>();
	}

    void Update()
    {
        if(elemental.transform.localScale.x > 0)
        {
            gameObject.transform.localScale = new Vector2 (attackScale[0], attackScale[1]);
        } else {
            gameObject.transform.localScale = new Vector2 (-attackScale[0], attackScale[1]);
        } 
    }

	void OnTriggerEnter2D (Collider2D hitInfo)
	{
        base.OnTriggerEnter2D(hitInfo);
		if (hitInfo.gameObject.name.Equals ("Player")) {
			PlayerCombat instance = GameObject.Find("Player").GetComponent<PlayerCombat>();
			instance.TakeDamage(attackDamage);
            if(instance.PlayerIsDebuffed){
                instance.ResetDebuffTimer();
            } else {
                instance.DebuffPlayer();
            }
			Instantiate(impactEffect, transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}
}
