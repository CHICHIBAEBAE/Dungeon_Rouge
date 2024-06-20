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

    // private readonly �� playerstat�� �ּҰ��� �����ֱ�
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

    // �ܺο��� ���� ��ȭ�� ����� �� ex. ����, ������, ����
    public void AddStatModifier(CharacterStat statModifier)
    {
        statsModifiers.Add(statModifier);
        UpdateCharacterStat();
    }

    // ���� ��ȭ ���� ex. ������ ������ �����Ѵٴ���
    public void RemoveStatModifier(CharacterStat statModifier)
    {
        statsModifiers.Remove(statModifier);
        UpdateCharacterStat();
    }

    private void UpdateCharacterStat()
    {
        // ���̽� ���� ���� ������ ��
        ApplyStatModifier(baseStats);

        // ����Ǵ� ��ġ����
        // statsChangeType enum ������ �°� Add, Multiple, Override ������ �ݿ�
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
        
        // TODO : ������ �ּҰ��� �����ִ� �ڵ尡 ����� ������ �����ϵ�, �ּҰ��� �����ִ� �ڵ� �ۼ�
        currentAttack.playerAtk = Mathf.Max(operation(currentAttack.playerAtk, newAttack.playerAtk), MinAttackDmg);
    }
        

    private void UpdateBasicStats(Func<float, float, float> operation, CharacterStat modifier)
    {
        // TODO : ������ �ּҰ��� �����ִ� �ڵ尡 ����� ������ �����ϵ�, �ּҰ��� �����ִ� �ڵ� �ۼ�
    }
}
