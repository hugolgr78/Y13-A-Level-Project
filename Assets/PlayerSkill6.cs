using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill6 : SkillsTree
{
    public GameObject skillsInformation;
    public GameObject followingSkill1;
    public GameObject parallelSkill;

    public void ActivateSkill()
    {
        ScoreManager instance = GameObject.Find("Canvas").GetComponent<ScoreManager>();
        PlayerCombat instance2 = GameObject.Find("Player").GetComponent<PlayerCombat>();
        if (instance.coins >= 270)
        {
            instance.coins -= 270;
            base.ActivateSkill(skillsInformation, followingSkill1, null);
            base.DeactivateSkill(parallelSkill);
            instance2.JesterArrowToEnemy = true;
            GetComponent<Button>().enabled = false;
        }
    }
}
