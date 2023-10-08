using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drakanian : Enemy
{   
    public int DrakanianAttackDamage = 15;

    public GameObject DrakanianBeast;
    protected override void Die()
    {
        DrakanianBeast.SetActive(true);
        ChangeBeastPosition();
        base.Die();
    }

    void ChangeBeastPosition()
    {
        enemyPosition = new float[3];
        enemyPosition[0] = gameObject.transform.position.x;
        enemyPosition[1] = gameObject.transform.position.y;

        DrakanianBeast.transform.position = new Vector2(enemyPosition[0], enemyPosition[1]);
    }

    public override void Attack(Collider2D player)
    {
        PlayerCombat instance = GameObject.Find("Player").GetComponent<PlayerCombat>();
        animator.SetTrigger("Attack");
        instance.TakeDamage(DrakanianAttackDamage);
    }
}
