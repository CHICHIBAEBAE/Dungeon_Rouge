using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInforUI : MonoBehaviour
{
    public StatData playerData;

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
        playerLevelText.text = "���� : " + playerData.Lv;
        playerAttackText.text = "���ݷ� : " + playerData.Atk;
        playerHealthText.text = "ü�� : " + playerData.MaxHealth;
    }
}
