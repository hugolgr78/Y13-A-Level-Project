using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill16 : SkillsTree
{
    public GameObject skillsInformation;

    public void ActivateSkill()
    {
        ScoreManager instance = GameObject.Find("Canvas").GetComponent<ScoreManager>();
        PlayerCombat instance2 = GameObject.Find("Player").GetComponent<PlayerCombat>();
        if(instance.coins >= 1500)
        {
            instance.coins -= 1500;
            base.ActivateSkill(skillsInformation, null, null);
            instance2.secondLife = true;
            GetComponent<Button>().enabled = false;
        }
    }
}
