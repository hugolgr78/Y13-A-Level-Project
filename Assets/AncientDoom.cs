using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AncientDoom : Enemy
{
    public GameObject scythe;

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
            Instantiate(scythe, transform.position, transform.rotation);
            nextFire = Time.time + fireRate; 
        }
    }

    public override void Attack(Collider2D player)
    {
        animator.SetTrigger("Attack");
        CheckIfFireBullet();
    }
}
