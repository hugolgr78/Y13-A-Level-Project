using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float playerHealth;
    public int amountOfAttackPotions;
    public int amountOfHealthPotions;
    public int amountOfShieldPotions;
    public float[] playerPosition;

    public PlayerData(PlayerCombat player)
    {
        playerHealth = PlayerCombat.currentHealthPlayer;
        amountOfAttackPotions = player.GetAttackPotionNumber();
        amountOfHealthPotions = player.GetHealthPotionNumber();
        amountOfShieldPotions = player.GetShieldPotionNumber();

        playerPosition = new float[3];
        playerPosition[0] = player.transform.position.x;
        playerPosition[1] = player.transform.position.y;
        playerPosition[2] = player.transform.position.z;
    }
}
