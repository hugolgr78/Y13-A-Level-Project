using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill10 : SkillsTree
{
    public GameObject skillsInformation;
    public GameObject followingSkill1;
    public GameObject parallelSkill;

    public void ActivateSkill()
    {
        ScoreManager instance = GameObject.Find("Canvas").GetComponent<ScoreManager>();
        PlayerMovement instance2 = GameObject.Find("Player").GetComponent<PlayerMovement>();
        if(instance.coins >= 450)
        {
            instance.coins -= 450;
            base.ActivateSkill(skillsInformation, followingSkill1, null);
            base.DeactivateSkill(parallelSkill);
            instance2.CanPlayerDash = true;
            GetComponent<Button>().enabled = false;
        }
    }
}
