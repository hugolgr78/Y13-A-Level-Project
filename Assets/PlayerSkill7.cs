using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill7 : SkillsTree
{
    public GameObject skillsInformation;
    public GameObject followingSkill1;
    public GameObject followingSkill2;

    public void ActivateSkill()
    {
        ScoreManager instance = GameObject.Find("Canvas").GetComponent<ScoreManager>();
        if(instance.coins >= 300)
        {
            instance.coins -= 300;
            base.ActivateSkill(skillsInformation, followingSkill1, followingSkill2);
            instance.doubleCoins = true;
            GetComponent<Button>().enabled = false;
        }
    }
}
