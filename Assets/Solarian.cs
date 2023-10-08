using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solarian : Enemy
{
    public int SolarianAttackDamage = 20;
    public int SolarianRollDamage = 30;
    public bool isRolling = false;
    public bool CanRoll = true;
    public float RollRange = 0.5f; 
    public Transform RollPoint;


    protected override void Update()
    {
        speed = 1.3f;

        Collider2D[] Roll = Physics2D.OverlapBoxAll(RollPoint.position, RollPoint.localScale, 0, playerLayers);
        foreach(Collider2D player in Roll)
        {
            if(!isRolling && CanRoll) 
            {
                isRolling = true;
                CanRoll = false;
                Invoke("StopRolling", 5);

                animator.SetBool("Roll", true);
                Debug.Log("roll");
            }
        }

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

    public void StopRolling()
    {
        isRolling = false;
        Invoke("ActivateRoll", 10);
        
        animator.SetBool("Roll", false);
        Debug.Log("Stop rolling");
    }

    public void ActivateRoll()
    {
        CanRoll = true;
        Debug.Log("Can Roll");
    }

    public override void Attack(Collider2D player)
    {
        PlayerCombat instance = GameObject.Find("Player").GetComponent<PlayerCombat>();
        if (isRolling)
        {
            StopRolling();
            animator.SetTrigger("Attack");
            instance.TakeDamage(SolarianRollDamage);
        } else {
            animator.SetTrigger("Attack");
            instance.TakeDamage(SolarianAttackDamage);
        }
    }
    
    void OnDrawGizmosSelected()
    {
        if (RollPoint == null)
            return;

        Gizmos.DrawWireCube(RollPoint.position, RollPoint.localScale);
    }
}

