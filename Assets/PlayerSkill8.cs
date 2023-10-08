using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill8 : SkillsTree
{
    public GameObject skillsInformation;
    public GameObject followingSkill1;

    public void ActivateSkill()
    {
        ScoreManager instance = GameObject.Find("Canvas").GetComponent<ScoreManager>();
        PlayerCombat instance2 = GameObject.Find("Player").GetComponent<PlayerCombat>();
        if(instance.coins >= 500)
        {
            instance.coins -= 500;
            base.ActivateSkill(skillsInformation, followingSkill1, null);
            instance2.HasChosenSkill2 = true;
            GetComponent<Button>().enabled = false;
        }
    }
}
