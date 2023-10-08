using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryMode : MonoBehaviour
{
    public GameObject Text1;
    public GameObject Text2;
    public GameObject Text3;
    public GameObject Text4;
    public GameObject PlayButton;

    void Update()
    {
        if (gameObject.active)
        {
            Text1.SetActive(true);
            Invoke("ActivateSecondText", 5);
        }
    }

    void ActivateSecondText()
    {
        Text2.SetActive(true);
        Invoke("ActivateThirdText", 5);
    }

    void ActivateThirdText()
    {
        Text3.SetActive(true);
        Invoke("ActivateFourthText", 5);
    }

    void ActivateFourthText()
    {
        Text4.SetActive(true);
        Invoke("ActivatePlayButton", 5);
    }

    void ActivatePlayButton()
    {
        PlayButton.SetActive(true);
    }

    public void GoToVenus()
    {
        // Teleport Player to Venus
    }
}
