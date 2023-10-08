using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkMageOrb : Enemy
{
    public float[] position;

    void Start()
    {
        position = new float[2];
        position[0] = gameObject.transform.position.x;
        position[1] = gameObject.transform.position.y;
    }
    
    public void Update ()
    {
        speed = 0f;
        slider.value = GetHealth();

        if(DarkMage.teleported){
            transform.position = new Vector2(position[0] + DarkMage.newPosistionX , position[1] + DarkMage.newPosistionY);
        }
    }
    protected override void Die()
    {
        Destroy(gameObject);
        DarkMage.counter += 1;
    }
}

