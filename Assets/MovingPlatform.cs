using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform position1;
    public Transform position2;
    public float speed;
    public Transform startPosition;
    Vector3 nextPosition;   
    public Transform PlayerCheck;
    public LayerMask PlayerLayers;
    public float FallingPlatformRange = 0.5f;

    void Start()
    {
        nextPosition = startPosition.position;   
    }

    void Update()
    {
        if(transform.position == position1.position)
        {
            nextPosition = position2.position;
        }
        
        if(transform.position == position2.position)
        {
            nextPosition = position1.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);

        Collider2D[] PlayerHit = Physics2D.OverlapCircleAll(PlayerCheck.position, FallingPlatformRange, PlayerLayers);
        foreach(Collider2D player in PlayerHit)
        {
            PlayerMovement instance = GameObject.Find("Player").GetComponent<PlayerMovement>();
            instance.OnLanding();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(position1.position, position2.position);
    }

    void OnDrawGizmosSelected()
    {
        if (PlayerCheck == null)
            return;

        Gizmos.DrawWireSphere(PlayerCheck.position, FallingPlatformRange);
    }
}
