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
        // statModifier를 반영하기 위해서 _characterStat를 받아옴
        PlayerData playerData = null;
        if(baseStats != null)
        {
            playerData = Instantiate(baseStats.playerData);
        }

        CurrentStat = new CharacterStat { playerData = playerData };
        // 능력치 강화
        CurrentStat.statsChangeType = baseStats.statsChangeType;
    }
}
