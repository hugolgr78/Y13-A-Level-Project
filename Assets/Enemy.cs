using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public GameObject healthBarUI;
    public Slider slider;
    public Animator animator;
    public float attackRange = 0.5f;
    public float attackRate = 1.5f;
    protected float nextAttackTime = 0;
    public Transform attackPoint;
    public LayerMask playerLayers; 
    public float speed = 1f;
    public bool moveRight;
    public GameObject heart;
    public GameObject coin;
    public GameObject deathEffect;
    public float[] enemyPosition;
    public float[] enemyScale;
    int Debufftimer = 10;
    public bool EnemyIsDebuffed;

    Color green = new Color(43f/255f, 94f/255f, 28f/255f);
    Color red = new Color(222f/255f, 28f/255f, 28f/255f);
    
    protected void Start()
    {
        currentHealth =  maxHealth;

        enemyScale = new float[2];
        enemyScale[0] = gameObject.transform.localScale.x;
        enemyScale[1] = gameObject.transform.localScale.y;

    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if(trigger.gameObject.CompareTag("Turn"))
        {
            if(moveRight)
            {
                moveRight = false;
            } else
            {
                moveRight = true;
            }
        }
    }

    protected virtual void Update()
    {
        slider.value = GetHealth();
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

    }

    public virtual void Attack(Collider2D player)
    {

    }

    public void TakeDamage(int damage) 
    {
        currentHealth -= damage;
        healthBarUI.SetActive(true);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        enemyPosition = new float[3];
        enemyPosition[0] = gameObject.transform.position.x;
        enemyPosition[1] = gameObject.transform.position.y;

        System.Random rnd = new System.Random();
        int num = rnd.Next(3);
        Debug.Log(num);

        if (num == 1)
        {
            Instantiate(heart, new Vector2(enemyPosition[0], enemyPosition[1]), transform.rotation); 
        }

        else if(num == 2)
        {
            Instantiate(coin, new Vector2(enemyPosition[0], enemyPosition[1]), transform.rotation); 
        }

        Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }   

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public float GetHealth()
    {
        return currentHealth / maxHealth;
    }

    public void DebuffEnemy()
    {
        EnemyIsDebuffed = true;
        Invoke("ChangeDebuffTimer", 1);
        slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = green;
    }

    public void ChangeDebuffTimer()
    {
        Debufftimer -= 1;
        TakeDamage(1);

        if (Debufftimer == 0)
        {
            Debufftimer = 10;
            EnemyIsDebuffed = false;
            slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = red;
        } else 
        {
            Invoke("ChangeDebuffTimer", 1);
        }
    }

    public void ResetDebuffTimer()
    {
        Debufftimer = 10;
    }
}

