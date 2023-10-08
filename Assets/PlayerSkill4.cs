using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill4 : SkillsTree
{
    public GameObject skillsInformation;
    public GameObject followingSkill1;

    public void ActivateSkill()
    {
        ScoreManager instance = GameObject.Find("Canvas").GetComponent<ScoreManager>();
        PlayerCombat instance2 = GameObject.Find("Player").GetComponent<PlayerCombat>();
        if(instance.coins >= 200)
        {
            instance.coins -= 200;
            base.ActivateSkill(skillsInformation, followingSkill1, null);
            instance2.CanPlayerDodge = true;
            GetComponent<Button>().enabled = false;
        }
    }
}
