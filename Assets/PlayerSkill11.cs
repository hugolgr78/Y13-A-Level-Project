using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill11 : SkillsTree
{
    public GameObject skillsInformation;
    public GameObject followingSkill1;
    public GameObject parallelSkill;

    public void ActivateSkill()
    {
        ScoreManager instance = GameObject.Find("Canvas").GetComponent<ScoreManager>();
        PlayerCombat instance2 = GameObject.Find("Player").GetComponent<PlayerCombat>();
        if(instance.coins >= 550)
        {
            instance.coins -= 550;
            base.ActivateSkill(skillsInformation, followingSkill1, null);
            base.DeactivateSkill(parallelSkill);
            instance2.CanPlayerRegenerate = true;
            GetComponent<Button>().enabled = false;
        }
    }
}
