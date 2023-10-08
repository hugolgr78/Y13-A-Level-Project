using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill15 : SkillsTree
{
    public GameObject skillsInformation;
    public GameObject followingSkill1;

    public void ActivateSkill()
    {
        ScoreManager instance = GameObject.Find("Canvas").GetComponent<ScoreManager>();
        if(instance.coins >= 800)
        {
            instance.coins -= 800;
            base.ActivateSkill(skillsInformation, followingSkill1, null);
            PlayerCombat.maxHealth = 300;
            GetComponent<Button>().enabled = false;
        }
    }
}
