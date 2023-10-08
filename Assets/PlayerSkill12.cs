using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill12 : SkillsTree
{
    public GameObject skillsInformation;
    public GameObject followingSkill1;

    public void ActivateSkill()
    {
        ScoreManager instance = GameObject.Find("Canvas").GetComponent<ScoreManager>();
        PlayerCombat instance2 = GameObject.Find("Player").GetComponent<PlayerCombat>();
        if(instance.coins >= 550)
        {
            instance.coins -= 550;
            base.ActivateSkill(skillsInformation, followingSkill1, null);
            instance2.fasterTimer = true;
            GetComponent<Button>().enabled = false;
        }
    }
}
