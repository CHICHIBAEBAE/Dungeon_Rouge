using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayStateUI : MonoBehaviour
{
    [SerializeField]private CharacterStatHandler characterStatHandler;
    public GameObject player;

    public Text playerLevelText;

    private void Start()
    {
        player = FindObjectOfType<Player>().gameObject;
        characterStatHandler = player.GetComponent<CharacterStatHandler>();
        UpdateUI();
    }

    void UpdateUI()
    {
        playerLevelText.text = "Lv : " + characterStatHandler.CurrentStat.statData.Lv;
        // 추후 EXP 추가하면 작성
    }
}
