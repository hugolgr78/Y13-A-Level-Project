using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill3 : SkillsTree
{
    public GameObject skillsInformation;
    public GameObject followingSkill1;

    public void ActivateSkill()
    {
        ScoreManager instance = GameObject.Find("Canvas").GetComponent<ScoreManager>();
        CharacterController2D instance2 = GameObject.Find("Player").GetComponent<CharacterController2D>();
        if(instance.coins >= 150)
        {
            instance.coins -= 150;
            base.ActivateSkill(skillsInformation, followingSkill1, null);
            instance2.canDoubleJump = true;
            GetComponent<Button>().enabled = false;
        }
    }
}
