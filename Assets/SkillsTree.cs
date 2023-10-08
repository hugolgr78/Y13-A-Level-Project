using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsTree : MonoBehaviour
{
    public static bool isTreeActive = false;
    public GameObject skillsTreeUI;
    public static bool aSkillIsShown = false;
    public GameObject rejectMessage;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && !aSkillIsShown)
        {
            if (!isTreeActive){
                ActivateTree(); 
            } else {
                DeActivateTree();
            }
        } 
    }  

    public void ActivateTree()
    {
        if(!PauseMenu.GameIsPaused && !Chest.ChestIsOpen && !Inventory.InventoryOn)
        {
            skillsTreeUI.SetActive(true);
            Time.timeScale = 0f;
            isTreeActive = true;
        }
    }

    public void DeActivateTree()
    {
        skillsTreeUI.SetActive(false);
        Time.timeScale = 1f;
        isTreeActive = false;
    }

    protected void ActivateSkill(GameObject skillObject, GameObject nextSkillButton1, GameObject nextSkillButton2)
    {
        if (nextSkillButton1 == null && nextSkillButton2 == null) 
        {
            skillObject.SetActive(false);
        } else if (nextSkillButton2 == null) 
        {
            nextSkillButton1.SetActive(true);
        } else 
        {
            nextSkillButton1.SetActive(true);
            nextSkillButton2.SetActive(true);
        }
        skillObject.SetActive(false);
        aSkillIsShown = false;
        ActivateTree();
    }

    protected void DeactivateSkill(GameObject removeSkill)
    {
        removeSkill.SetActive(false);
    }

    public void FreezeTime()
    {
        Time.timeScale = 0f;
    }

    public void ChangeVariableTrue()
    {
        aSkillIsShown = true;
        isTreeActive = true;
    }

    public void ChangeVariableFalse()
    {
        aSkillIsShown = false;
    }
    
    public void ChangeMessage()
    {
        rejectMessage.SetActive(false);
    }
}
