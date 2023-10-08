using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public Transform PlayerCheck;
    public LayerMask PlayerLayers;

    void OnDrawGizmosSelected()
    {
        if (PlayerCheck == null)
            return;

        Gizmos.DrawWireCube(PlayerCheck.position, PlayerCheck.localScale);
    }
    void Update()
    {
        Collider2D[] PlayerHit = Physics2D.OverlapBoxAll(PlayerCheck.position, PlayerCheck.localScale, 0, PlayerLayers);
        foreach(Collider2D player in PlayerHit)
        {
            PlayerMovement instance = GameObject.Find("Player").GetComponent<PlayerMovement>();
            instance.OnLanding();
            Invoke("DestroyPlatform", 0.6f);
        }
    }

    void DestroyPlatform()
    {
        Grid instance = GameObject.Find("Grid").GetComponent<Grid>(); 
        instance.Wait(gameObject);
        gameObject.SetActive(false);
    }
}