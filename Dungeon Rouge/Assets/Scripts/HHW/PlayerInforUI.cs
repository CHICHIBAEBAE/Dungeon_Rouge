using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInforUI : MonoBehaviour
{
    [SerializeField]private CharacterStatHandler _characterStatHandler;

    public Text playerLevelText;
    public Text playerAttackText;
    public Text playerHealthText;

    private void Start()
    {
        if(_characterStatHandler != null)
        {
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        playerAttackText.text = "공격력 : " + _characterStatHandler.CurrentStat.statData.Atk;
        playerHealthText.text = "체력 : " + _characterStatHandler.CurrentStat.statData.MaxHealth;
    }
}
