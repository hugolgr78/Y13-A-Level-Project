using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolutionBeast : Enemy
{
    public GameObject bullet;
    public GameObject FirePoint;
    
    float fireRate;
    float nextFire;

    void Start()
    {
        base.Start();
        fireRate = 1.5f;
        nextFire = Time.time;  
    }

    void CheckIfFireBullet()
    {
        if(Time.time > nextFire)
        {
            Instantiate(bullet, FirePoint.transform.position, transform.rotation);
            nextFire = Time.time + fireRate; 
        }
    }

    public override void Attack(Collider2D player)
    {
        animator.SetTrigger("Attack");
        CheckIfFireBullet();
    }
    
    protected override void Update()
    {
        speed=1f;
        
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);
        foreach(Collider2D player in hitPlayer)
        {
            speed = 0f;
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
