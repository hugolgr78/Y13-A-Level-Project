using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    int jumpHeight = 5;
    public Transform groundEnemyCheck;
    public float groundRange = 0.5f;
    public bool isEnemyGrounded;
    bool isJumping = false;
    bool canJump = true;
    public LayerMask groundLayers;
    [SerializeField] Rigidbody2D rb;
    public int SlimeAttackDamage;

    void OnDrawGizmosSelected()
    {
        if (groundEnemyCheck == null)
            return;

        Gizmos.DrawWireSphere(groundEnemyCheck.position, groundRange);
    }

    protected override void Update()
    {
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
        
        isEnemyGrounded = Physics2D.OverlapCircle(groundEnemyCheck.position, groundRange, groundLayers);
        if(isEnemyGrounded)
        {   
            rb.gravityScale = 0;
            if (isJumping) {
                    speed = 0f;
                    rb.gravityScale = 0;
                    isJumping = false;
                    canJump = false;
                    GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
                    Invoke("resetCanJump", 1);
            } else {
                if (canJump) {
                    rb.gravityScale = 1;
                    speed = 1f;
                    GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
                    Invoke("setIsJumpingToTrue", 0.5f);
                }
            }
        } else{
            rb.gravityScale = 1;
        }
    }

    void setIsJumpingToTrue() {
        isJumping = true;
    }

    void resetCanJump() {
        canJump = true;
    }

    public override void Attack(Collider2D player)
    {
        PlayerCombat instance = GameObject.Find("Player").GetComponent<PlayerCombat>();
        animator.SetTrigger("Attack");
        instance.TakeDamage(SlimeAttackDamage);
    }
}
