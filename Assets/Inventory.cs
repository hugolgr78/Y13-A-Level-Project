using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static bool InventoryOn = false;
    public GameObject InventoryUI;
    static int ironBars = 0;
    public Text ironBarsText;

    public void InventoryFalse ()
    {
        InventoryUI.SetActive(false);
        Time.timeScale = 1f;
        InventoryOn = false; 
    }

    void InventroyTrue ()
    {
        if (!PauseMenu.GameIsPaused && !Chest.ChestIsOpen && !SkillsTree.isTreeActive)
        {
        InventoryUI.SetActive(true);
        Time.timeScale = 0f;
        InventoryOn = true;
        }
    }

    public void AddHealthToPlayer ()
    {
        PlayerCombat instance = GameObject.Find("Player").GetComponent<PlayerCombat>();
        if(instance.GetHealthPotionNumber() > 0 && PlayerCombat.currentHealthPlayer != PlayerCombat.maxHealth)
        {
            if (instance.GetCanTakeHealthPotion())
            {
                if (instance.fasterTimer) {
                    instance.Healthtimer = 45;
                } else {
                    instance.Healthtimer = 60;
                }
                instance.UpdateHealthViaPotion(100);
                instance.UpdateHealthPotion(-1);
            }
        }
    }

    public void ChangePlayerDamage ()
    {
        PlayerCombat instance = GameObject.Find("Player").GetComponent<PlayerCombat>();
        if (instance.GetAttackPotionNumber() > 0 && instance.GetCanTakeAttackPotion())
        {
            instance.UpdateDamage();
            instance.UpdateAttackPotion(-1);
        }
    }

    public void ChangeEnemyDamage ()
    {
        PlayerCombat instance = GameObject.Find("Player").GetComponent<PlayerCombat>();

        if (instance.GetShieldPotionNumber() > 0 && instance.GetCanTakeShieldPotion())
        {
            instance.ShieldPotion();
            instance.UpdateShieldPotion(-1);
        }
    }

    public void AddIronBar(int number)
    {
        ironBars += number;
        ironBarsText.text = ironBars.ToString();
    }

    public static int GetIronBarNumber()
    {
        return ironBars;
    }

    void Update()
    {
       if (Input.GetKeyDown(KeyCode.E))
        {
            if (InventoryOn)
            {
                InventoryFalse();
            } 
            else
            {
                InventroyTrue();
            }
        } 
    }
}
