using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandElemental : Enemy
{
    public Transform target;
    public Transform attackTarget;
    public GameObject attack1;
    
    float fireRate;
    float nextFire;

    void Start()
    {
        base.Start();
        fireRate = 3f;
        nextFire = Time.time;  
    }

    void Update()
    {
        base.Update();

        if(target.transform.position.x > gameObject.transform.position.x)
        {
            transform.localScale = new Vector2 (-enemyScale[0], enemyScale[1]);
            moveRight = true;
        } else {
            transform.localScale = new Vector2 (enemyScale[0], enemyScale[1]);
            moveRight = false;
        }
    }

    void CheckIfFireAttack1()
    {
        if(Time.time > nextFire)
        {
            animator.SetTrigger("Attack");
            Instantiate(attack1, transform.position, transform.rotation);
            nextFire = Time.time + fireRate; 
        }
    }

    void CheckIfFireAttack2()
    {
        if(Time.time > nextFire)
        {
            animator.SetTrigger("Attack");
            nextFire = Time.time + fireRate; 
            Instantiate(attackTarget, target.transform.position, transform.rotation);
        }
    }

    public override void Attack(Collider2D player)
    {
        ChooseAttack();
    }

    void ChooseAttack()
    {
        System.Random rnd = new System.Random();
        int num = rnd.Next(2);

        if (num == 0) {
            CheckIfFireAttack1();
        } else {
            CheckIfFireAttack2();
        }
    }

    // For the sand elemental, when the player enters the range, the elemental stops and randomly chooses (random number generator) between her two attacks. The first attack is a bullet that will deal 20 hp and aply a 
    // debuff that will take the players life for an extra 1 damage every second for 10 seconds. The second attack is an explosion. A target will appear at the player's position to warn the player of the attack.
    // One second later (or less), the explosion ovvurs.

    //  The elemental should always face the player.
}
