using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsChest : Chest
{
    public Text WeaponLevelText;
    public void Skill1Chosen()
    {
        Content1.SetActive(false);
        Content2.SetActive(false);
        PlayerCombat instance = GameObject.Find("Player").GetComponent<PlayerCombat>();
        instance.HasChosenSkill1 = true;
        WeaponLevelText.text = "2";
    }
    public void Skill2Chosen()
    {
        Content1.SetActive(false);
        Content2.SetActive(false);        
        PlayerCombat instance = GameObject.Find("Player").GetComponent<PlayerCombat>();
        instance.HasChosenSkill2 = true;
        WeaponLevelText.text = "2";
    }
}
