using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text CoinText;
    public int coins;

    public GameObject particleSystem;
    public GameObject finishedText;
    public GameObject missionText;
    public static bool isSpaceshipFixed = false;
    public bool isMissionTextShown = false;
    public bool doubleCoins = false;
    public bool coinsFromChest;

    private void SetCoinText()
    {
        CoinText.text = coins.ToString();
    }

    void Start()
    {
        coins = 10000;
        if(!isMissionTextShown)
        {
            missionText.SetActive(true);
            isMissionTextShown = true;
            Invoke("ChangeMissionText", 5);
        }
    }

    public void AddCoin(int number)
    {
        if (doubleCoins && !coinsFromChest)
        {
            number *= 2;
            coins += number;
        } else {
            coins += number; 
        }
        coinsFromChest = false;
    }

    public int GetAmountOfCoins()
    {
        return coins;
    }

    void Update()
    {
        if(coins >= 200 && Inventory.GetIronBarNumber() >= 10 && !isSpaceshipFixed)
        {
            particleSystem.SetActive(false);
            finishedText.SetActive(true);
            isSpaceshipFixed = true;
            Invoke("ChangeFinishedText", 5);
        }
        SetCoinText();
    }

    void ChangeFinishedText()
    {
        finishedText.SetActive(false);
    }

    void ChangeMissionText()
    {
        missionText.SetActive(false);
    }
}
