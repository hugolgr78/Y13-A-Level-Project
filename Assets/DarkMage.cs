using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkMage : Enemy
{
    public GameObject BombAttack;
    public GameObject ShieldAttack;
    public GameObject ProjectileAttack;
    public GameObject ProjectileAttackRight;
    public GameObject SummonAttack;

    public Transform target;

    public float[] startPosition;
    public bool canTeleport = false;
    public static bool teleported = false;
    public static int newPosistionX;
    public static int newPosistionY;

    public bool Attack3Active;
    public static int counter = 0;
    public bool CanAttack = true;
    public bool CanStartAttacking = true;
    // CANSTARTATTACKING TO BE SET AS TRUE AS SOON AS THE PLAYER ENTERS BOSS ROOM

    public void Start()
    {
        base.Start();
        startPosition = new float[2];
        startPosition[0] = gameObject.transform.position.x;
        startPosition[1] = gameObject.transform.position.y;
        Invoke("ResetTeleport", 2);
    }
    protected override void Update()
    {    
        if(target.transform.position.x > gameObject.transform.position.x)
        {
            transform.localScale = new Vector2 (-enemyScale[0], enemyScale[1]);
        } else {
            transform.localScale = new Vector2 (enemyScale[0], enemyScale[1]);
        }

        if(CanAttack && CanStartAttacking)
        {
            ChooseAttack();
        }

        if(canTeleport)
        {
            canTeleport = false;
            System.Random rnd = new System.Random();
            newPosistionX = rnd.Next(9) + 5;
            newPosistionY = rnd.Next(1) + 1;
            int leftOrRight = rnd.Next(1);
            int upOrDown = rnd.Next(1);

            if (leftOrRight == 0 && upOrDown == 0){ // left and up
                newPosistionX = -newPosistionX;
                gameObject.transform.position = new Vector2(startPosition[0] + newPosistionX, startPosition[1] + newPosistionY);
            } else if (leftOrRight == 1 && upOrDown == 0){ // right and up
                gameObject.transform.position = new Vector2(startPosition[0] + newPosistionX, startPosition[1] + newPosistionY);
            } else if (leftOrRight == 0 && upOrDown == 1){ // left and down
                newPosistionX = -newPosistionX;
                newPosistionY = -newPosistionY;
                gameObject.transform.position = new Vector2(startPosition[0] + newPosistionX, startPosition[1] + newPosistionY);
            } else { // right and down
                newPosistionY = -newPosistionY;
                gameObject.transform.position = new Vector2(startPosition[0] + newPosistionX, startPosition[1] + newPosistionY);
            }
            teleported = true;
            Invoke("ResetTeleport", 5);
        }

        slider.value = GetHealth();

        if(counter >= 5)
        {
            Attack3Active = false;  
            counter = 0;
        }
    }

    public void ChooseAttack()
    {
        System.Random rnd = new System.Random();
        int num = rnd.Next(4);

        if (num == 0) {
            Attack1();
        } else if (num == 1) {
            Attack2();
        } else if (num == 2 && !Attack3Active) {
            Attack3();
        } else {
            Attack4();
        }
        CanAttack = false;
        Invoke("ResetAttack", 5);
    }

    public void ResetTeleport()
    {
        canTeleport = true;
        teleported = false;
    }

    public void Attack1()
    {
        animator.SetBool("Attack" , true);
        Debug.Log("Attack1");
        Invoke("StopAnimation", 1);
        
        Instantiate(BombAttack, new Vector3 (target.transform.position.x, transform.position.y + 2, 0), transform.rotation);
    }

    public void Attack2()
    {
        animator.SetBool("Attack" , true);
        Debug.Log("Attack2");
        Invoke("StopAnimation", 1);
        
        Instantiate(SummonAttack, new Vector3 (transform.position.x + 0.6f, transform.position.y - 1, 0), transform.rotation);
        Instantiate(SummonAttack, new Vector3 (transform.position.x + 1.2f, transform.position.y - 1, 0), transform.rotation);
        Instantiate(SummonAttack, new Vector3 (transform.position.x - 0.6f, transform.position.y - 1, 0), transform.rotation);
        Instantiate(SummonAttack, new Vector3 (transform.position.x - 1.2f, transform.position.y - 1, 0), transform.rotation);
    }

    public void Attack3()
    {
        animator.SetBool("Attack" , true);
        Debug.Log("Attack3");
        Invoke("StopAnimation", 1);
        
        Attack3Active = true;
        Instantiate(ShieldAttack, new Vector3 (transform.position.x, transform.position.y - 2, 0), transform.rotation);
        Instantiate(ShieldAttack, new Vector3 (transform.position.x, transform.position.y + 2, 0), transform.rotation);
        Instantiate(ShieldAttack, new Vector3 (transform.position.x - 2, transform.position.y, 0), transform.rotation);
        Instantiate(ShieldAttack, new Vector3 (transform.position.x + 2, transform.position.y, 0), transform.rotation);
        Instantiate(ShieldAttack, new Vector3 (transform.position.x + 1, transform.position.y + 1, 0), transform.rotation);
        Instantiate(ShieldAttack, new Vector3 (transform.position.x + 1, transform.position.y - 1, 0), transform.rotation);
        Instantiate(ShieldAttack, new Vector3 (transform.position.x - 1, transform.position.y + 1, 0), transform.rotation);
        Instantiate(ShieldAttack, new Vector3 (transform.position.x - 1, transform.position.y - 1, 0), transform.rotation);
    }

    public void Attack4()
    {
        animator.SetBool("Attack" , true);
        Debug.Log("Attack4");
        Invoke("StopAnimation", 1);

        if(target.transform.position.x < gameObject.transform.position.x)
        {
            Instantiate(ProjectileAttack, new Vector3 (transform.position.x - 2, transform.position.y, 0), transform.rotation);
            Instantiate(ProjectileAttack, new Vector3 (transform.position.x - 1.7f, transform.position.y + 1.1f, 0), transform.rotation);
            Instantiate(ProjectileAttack, new Vector3 (transform.position.x - 1.7f, transform.position.y - 1.1f, 0), transform.rotation);
            Instantiate(ProjectileAttack, new Vector3 (transform.position.x - 1, transform.position.y + 2, 0), transform.rotation);
            Instantiate(ProjectileAttack, new Vector3 (transform.position.x - 1, transform.position.y - 2, 0), transform.rotation);
        } else {
            Instantiate(ProjectileAttackRight, new Vector3 (transform.position.x + 2, transform.position.y, 0), transform.rotation);
            Instantiate(ProjectileAttackRight, new Vector3 (transform.position.x + 1.7f, transform.position.y + 1.1f, 0), transform.rotation);
            Instantiate(ProjectileAttackRight, new Vector3 (transform.position.x + 1.7f, transform.position.y - 1.1f, 0), transform.rotation);
            Instantiate(ProjectileAttackRight, new Vector3 (transform.position.x + 1, transform.position.y + 2, 0), transform.rotation);
            Instantiate(ProjectileAttackRight, new Vector3 (transform.position.x + 1, transform.position.y - 2, 0), transform.rotation);
        }
    }

    public void ResetAttack()
    {
        CanAttack = true;
    }

    public void StopAnimation()
    {
        animator.SetBool("Attack", false);
    }

}

    // The Dark Mage is the final boss in the game. It will move the same way that the sand elemental and will have multiple attacks. One of his attacks will be to summon a bomb 
    // which will spread across the floor upon hitting it (instantiate a flame that moves left and one that moves right for a set amount of seconds). Another attack will be 
    // to summon enemies to kill the player. These enemies need to have a low amount of health and do decent damage. His third attack will be to summon orbs around him
    // that will act as a shield (in a circle). These projectiles will have health and will be able to be destroyed. This attack will a cooldown of 30 seconds. 
    // His fourth and final attack will be to summon a barrage of 5 projectiles in front of him in a semi-circle that will shoot out towards the player. 
