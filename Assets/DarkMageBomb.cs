using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkMageBomb : MonoBehaviour
{
    public GameObject flamesRight;
    public GameObject flamesLeft;

    public void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if(hitInfo.gameObject.layer == 10)
        {
            Destroy(gameObject);
			Instantiate(flamesRight, new Vector3 (transform.position.x + 2, transform.position.y, 0 ), transform.rotation);  
            Instantiate(flamesLeft, new Vector3 (transform.position.x - 2, transform.position.y, 0 ), transform.rotation);  
        }
    }
}
    