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
        player = FindObjectOfType<Player>().gameObject;
        CharacterStatHandler = player.GetComponent<CharacterStatHandler>();
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        playerHpTxt.text = $" ü��  : <color=red>{Player.instance.curHP}</color> / <color=red>{CharacterStatHandler.CurrentStat.statData.MaxHealth}</color>";
        playerAtkTxt.text = $"���ݷ� : {CharacterStatHandler.CurrentStat.statData.Atk}";
    }
}