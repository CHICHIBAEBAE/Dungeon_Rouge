using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInforUI : MonoBehaviour
{
    public PlayerData playerData;

    public Text playerLevelText;
    public Text playerAttackText;
    public Text playerHealthText;

    private void Start()
    {
        if(playerData != null)
        {
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        playerLevelText.text = "레벨 : " + playerData.playerLv;
        playerAttackText.text = "공격력 : " + playerData.playerAtk;
        playerHealthText.text = "체력 : " + playerData.playerMaxHealth;
    }
}
