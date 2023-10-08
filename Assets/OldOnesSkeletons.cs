using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldOnesSkeletons : Enemy
{
    [SerializeField] Rigidbody2D rb;
    public bool isEnemyGrounded;
    public LayerMask groundLayers;
    public Transform groundEnemyCheck;
    public float groundRange = 0.5f;
    PlayerCombat target;
    public int SkeletonAttackDamage = 15;

    void Start()
    {
        base.Start();
        target = GameObject.FindObjectOfType<PlayerCombat>();
    }

    protected override void Update()
    { 
        isEnemyGrounded = Physics2D.OverlapCircle(groundEnemyCheck.position, groundRange, groundLayers);
        if(isEnemyGrounded)
        {
            Debug.Log("Grounded");
            rb.gravityScale = 0;
            rb.velocity = Vector2.zero;
        } else {
            rb.gravityScale = 1;
        }

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

        if(target.transform.position.x > gameObject.transform.position.x)
        {
            transform.localScale = new Vector2 (-enemyScale[0], enemyScale[1]);
            moveRight = true;
        } else {
            transform.localScale = new Vector2 (enemyScale[0], enemyScale[1]);
            moveRight = false;
        }

        slider.value = GetHealth();
    }

    void OnDrawGizmosSelected()
    {
        if (groundEnemyCheck == null)
            return;

        Gizmos.DrawWireSphere(groundEnemyCheck.position, groundRange);
    }

    public override void Attack(Collider2D player)
    {
        PlayerCombat instance = GameObject.Find("Player").GetComponent<PlayerCombat>();
        animator.SetTrigger("Attack");
        instance.TakeDamage(SkeletonAttackDamage);
    }
}
