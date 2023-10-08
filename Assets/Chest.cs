using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public LayerMask playerLayers;
    public float chestRange = 0.5f;
    public Transform chestPoint;
    public static bool ChestIsOpen = false;
    public GameObject ChestContentsUI;
    public GameObject Content1;
    public GameObject Content2;

    public SpriteRenderer spriteRenderer;
    public Sprite OpenedChest;
    
    //  COINS MUST ALWAYS CONTENT ONE. BARS AND POTIONS ARE CONTENT 2

    void Update()
    {
        Collider2D[] playerInBound = Physics2D.OverlapCircleAll(chestPoint.position, chestRange, playerLayers);
        foreach(Collider2D player in playerInBound)
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                if(!ChestIsOpen)
                {
                    OpenChest();
                } else
                {
                    CloseChest();
                }
            }
        }
    }

    public void OpenChest()
    {
        if (!PauseMenu.GameIsPaused && !Inventory.InventoryOn && !SkillsTree.isTreeActive)
        {
            ChestContentsUI.SetActive(true);
            Time.timeScale = 0f;
            ChestIsOpen = true;
            spriteRenderer.sprite = OpenedChest; 
        }
    }

    public void CloseChest()
    {   
        ChestContentsUI.SetActive(false);
        Time.timeScale = 1f;
        ChestIsOpen = false; 
    }

    public void Coinbutton()
    {
        ScoreManager instance= GameObject.Find("Canvas").GetComponent<ScoreManager>();
        instance.coinsFromChest = true;
        instance.AddCoin(100);
        Content1.SetActive(false); 
    }

    public void IronButton ()
    {
        Inventory instance = GameObject.Find("Canvas").GetComponent<Inventory>();
        instance.AddIronBar(10);
        Content2.SetActive(false);
    }

    public void AddAttackPotion ()
    {
        PlayerCombat instance = GameObject.Find("Player").GetComponent<PlayerCombat>();
        instance.UpdateAttackPotion(1);
        Content2.SetActive(false);
    }

    public void AddHealthPotion ()
    {
        PlayerCombat instance = GameObject.Find("Player").GetComponent<PlayerCombat>();
        instance.UpdateHealthPotion(1);
        Content2.SetActive(false);
    }

    public void AddShieldPotion ()
    {
        PlayerCombat instance = GameObject.Find("Player").GetComponent<PlayerCombat>();
        instance.UpdateShieldPotion(1);
        Content2.SetActive(false);
    }

    void OnDrawGizmosSelected()
    {
        if (chestPoint == null)
            return;

        Gizmos.DrawWireSphere(chestPoint.position, chestRange);
    }

}
