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
        playerLevelText.text = "���� : " + playerData.playerLv;
        playerAttackText.text = "���ݷ� : " + playerData.playerAtk;
        playerHealthText.text = "ü�� : " + playerData.playerMaxHealth;
    }
}
