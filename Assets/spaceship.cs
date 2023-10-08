using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class spaceship : MonoBehaviour
{
    public Transform grabPoint;
    public float grabRange = 0.5f;
    public LayerMask playerLayers;
    public GameObject StoryMode2Ui;

    // Update is called once per frame
    void Update()
    {
        Collider2D[] playerInBound = Physics2D.OverlapCircleAll(grabPoint.position, grabRange, playerLayers);
        foreach(Collider2D player in playerInBound)
        {
            if(Input.GetKeyDown(KeyCode.Q) && ScoreManager.isSpaceshipFixed)
            {
                StoryMode2Ui.SetActive(true);   
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (grabPoint == null)
            return;

        Gizmos.DrawWireSphere(grabPoint.position, grabRange);
    }
}
