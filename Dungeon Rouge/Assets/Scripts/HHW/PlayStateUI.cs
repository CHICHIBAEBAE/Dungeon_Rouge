using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayStateUI : MonoBehaviour
{
    [SerializeField]private CharacterStatHandler characterStatHandler;

    public Text playerLevelText;

    private void Start()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        playerLevelText.text = "Lv : " + characterStatHandler.CurrentStat.statData.Lv;
        // ���� EXP �߰��ϸ� �ۼ�
    }
}
