using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill2 : SkillsTree
{
    public GameObject skillsInformation;
    public GameObject followingSkill1;
    public GameObject followingSkill2;

    public void ActivateSkill()
    {
        ScoreManager instance = GameObject.Find("Canvas").GetComponent<ScoreManager>();
        if(instance.coins >= 150)
        {
            instance.coins -= 150;
            base.ActivateSkill(skillsInformation, followingSkill1, followingSkill2);
            PlayerMovement instance2 = GameObject.Find("Player").GetComponent<PlayerMovement>();
            instance2.canWallJump = true;
            GetComponent<Button>().enabled = false;
        } else {
            rejectMessage.SetActive(true);
        }
    }
}
