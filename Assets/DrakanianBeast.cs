using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrakanianBeast : Enemy
{
    public int DrakanianBeastAttackDamage =15;

    public override void Attack(Collider2D player)
    {
        PlayerCombat instance = GameObject.Find("Player").GetComponent<PlayerCombat>();
        animator.SetTrigger("Attack");
        instance.TakeDamage(DrakanianBeastAttackDamage);
    }
}
