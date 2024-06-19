using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterStatHandler : MonoBehaviour
{
    [SerializeField] private CharacterStat baseStats;

    public CharacterStat CurrentStat { get; private set; } = new();

    public List<CharacterStat> statsModifiers = new List<CharacterStat>();

    // private readonly 로 playerstat의 최소값을 정해주기
    private readonly float MinAttackDmg = 1.0f;

    private void Awake()
    {
        if(baseStats.playerData != null)
        {
            baseStats.playerData = Instantiate(baseStats.playerData);
            CurrentStat.playerData = Instantiate(baseStats.playerData);
        }
        UpdateCharacterStat();
    }

    // 외부에서 스탯 변화를 얻었을 때 ex. 유물, 아이템, 버프
    public void AddStatModifier(CharacterStat statModifier)
    {
        statsModifiers.Add(statModifier);
        UpdateCharacterStat();
    }

    // 스탯 변화 해제 ex. 아이템 장착을 해제한다던지
    public void RemoveStatModifier(CharacterStat statModifier)
    {
        statsModifiers.Remove(statModifier);
        UpdateCharacterStat();
    }

    private void UpdateCharacterStat()
    {
        // 베이스 스탯 먼저 적용한 후
        ApplyStatModifier(baseStats);

        // 변경되는 수치들을
        // statsChangeType enum 순서에 맞게 Add, Multiple, Override 순으로 반영
        foreach(CharacterStat stat in statsModifiers.OrderBy(o => o.statsChangeType))
        {
            ApplyStatModifier(stat);
        }
    }

    private void ApplyStatModifier(CharacterStat modifier)
    {
        Func<float, float, float> operation = modifier.statsChangeType switch
        {
            StatsChangeType.Add => (current, change) => current + change,
            StatsChangeType.Multiple => (current, change) => current * change,
            StatsChangeType.Override => (current, change) => change
        };

        UpdateBasicStats(operation, modifier);
        UpdateAttackStats(operation, modifier);
    }

    private void UpdateAttackStats(Func<float, float, float> operation, CharacterStat modifier)
    {
        if(CurrentStat.playerData == null || modifier.playerData == null) return;

        var currentAttack = CurrentStat.playerData;
        var newAttack = modifier.playerData;
        
        // TODO : 위에서 최소값을 정해주는 코드가 생기면 변경을 적용하되, 최소값을 정해주는 코드 작성
        currentAttack.playerAtk = Mathf.Max(operation(currentAttack.playerAtk, newAttack.playerAtk), MinAttackDmg);
    }
        

    private void UpdateBasicStats(Func<float, float, float> operation, CharacterStat modifier)
    {
        // TODO : 위에서 최소값을 정해주는 코드가 생기면 변경을 적용하되, 최소값을 정해주는 코드 작성
    }
}
