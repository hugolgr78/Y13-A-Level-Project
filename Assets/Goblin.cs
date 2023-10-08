using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{
    public int GoblinAttackDamage = 5;

    public override void Attack(Collider2D player)
    {
        PlayerCombat instance = GameObject.Find("Player").GetComponent<PlayerCombat>();
        animator.SetTrigger("Attack");
        instance.TakeDamage(GoblinAttackDamage);
    }
}
