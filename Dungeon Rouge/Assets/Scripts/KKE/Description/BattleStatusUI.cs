using UnityEngine;
using UnityEngine.UI;

public class BattleStatusUI : MonoBehaviour
{
    public CharacterStatHandler CharacterStatHandler;
    public GameObject player;
    public Text playerLevelTxt;
    public Text playerHpTxt;
    public Text playerAtkTxt;

    private void Start()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        playerLevelTxt.text = $" 레벨  : {CharacterStatHandler.CurrentStat.statData.Lv}";
        playerHpTxt.text = $" 체력  : {CharacterStatHandler.CurrentStat.statData.MaxHealth} / 100";
        playerAtkTxt.text = $"공격력 : {CharacterStatHandler.CurrentStat.statData.Atk}";
    }
}