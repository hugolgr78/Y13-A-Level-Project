using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill14 : SkillsTree
{
    public GameObject skillsInformation;
    public GameObject followingSkill1;

    public void ActivateSkill()
    {
        ScoreManager instance = GameObject.Find("Canvas").GetComponent<ScoreManager>();
        PlayerMovement instance2 = GameObject.Find("Player").GetComponent<PlayerMovement>();
        if(instance.coins >= 800)
        {
            instance.coins -= 800;
            base.ActivateSkill(skillsInformation, followingSkill1, null);
            instance2.canTeleport = true;
            instance2.canTeleportAgain = true;
            GetComponent<Button>().enabled = false;
        }
    }
}
