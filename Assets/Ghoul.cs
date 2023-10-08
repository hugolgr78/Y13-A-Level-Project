using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghoul : Enemy
{
    public int GhoulAttackDamage = 25;

    public override void Attack(Collider2D player)
    {
        PlayerCombat instance = GameObject.Find("Player").GetComponent<PlayerCombat>();
        animator.SetTrigger("Attack");
        instance.TakeDamage(GhoulAttackDamage);
        System.Random rnd = new System.Random();
        int num = rnd.Next(6);
        if (num == 5)
        {
            instance.DebuffPlayer();
        }
    }
}
