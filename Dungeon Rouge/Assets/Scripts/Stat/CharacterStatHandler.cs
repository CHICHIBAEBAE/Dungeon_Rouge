using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatHandler : MonoBehaviour
{
    [SerializeField] private CharacterStat baseStats;

    public CharacterStat CurrentStat { get; private set; }
    public List<CharacterStat> statsModifiers = new List<CharacterStat>();

    private void Awake()
    {
        UpdateCharacterStat();
    }

    private void UpdateCharacterStat()
    {
        // statModifier�� �ݿ��ϱ� ���ؼ� _characterStat�� �޾ƿ�
        PlayerData playerData = null;
        if(baseStats != null)
        {
            playerData = Instantiate(baseStats.playerData);
        }

        CurrentStat = new CharacterStat { playerData = playerData };
        // �ɷ�ġ ��ȭ
        CurrentStat.statsChangeType = baseStats.statsChangeType;
    }
}
