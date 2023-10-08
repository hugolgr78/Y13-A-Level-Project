using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{
    public GameObject cobweb;
    
    float fireRate;
    float nextFire;

    void Start()
    {
        base.Start();
        fireRate = 3f;
        nextFire = Time.time;  
    }

    void CheckIfFireCobweb()
    {
        if(Time.time > nextFire)
        {
            Instantiate(cobweb, transform.position, transform.rotation);
            nextFire = Time.time + fireRate; 
        }
    }

    public override void Attack(Collider2D player)
    {
        animator.SetTrigger("Attack");
        CheckIfFireCobweb();
    }

    protected override void Update()
    {
        speed=1f;
        gameObject.GetComponent<Animator>().enabled = true;
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);
        foreach(Collider2D player in hitPlayer)
        {
            speed = 0f;
            gameObject.GetComponent<Animator>().enabled = false;
            if (Time.time >= nextAttackTime)
            {
                Attack(player);
                nextAttackTime = Time.time + 3f / attackRate;
            } 
        }

        if (moveRight) 
        {
            transform.Translate(2 * Time.deltaTime * speed, 0,0);
            transform.localScale = new Vector2 (-enemyScale[0], enemyScale[1]);
        } else 
        {
            transform.Translate(-2 * Time.deltaTime * speed, 0,0);
            transform.localScale = new Vector2 (enemyScale[0], enemyScale[1]);
        }

        slider.value = GetHealth();
    }

}
