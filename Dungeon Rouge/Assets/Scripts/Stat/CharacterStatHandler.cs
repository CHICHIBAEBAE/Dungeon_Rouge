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

    private readonly int MinLv = 1;
    private readonly float MinAttackDmg = 1.0f;
    private readonly int MinMaxHealth = 1;

    private void Awake()
    {
        if (baseStats.statData != null)
        {
            baseStats.statData = Instantiate(baseStats.statData);
            CurrentStat.statData = Instantiate(baseStats.statData);
        }
        UpdateCharacterStat();
    }


    public void AddStatModifier(CharacterStat statModifier)
    {
        statsModifiers.Add(statModifier);
        UpdateCharacterStat();
    }


    public void RemoveStatModifier(CharacterStat statModifier)
    {
        statsModifiers.Remove(statModifier);
        UpdateCharacterStat();
    }

    private void UpdateCharacterStat()
    {
        ApplyStatModifier(baseStats);

        foreach (CharacterStat stat in statsModifiers.OrderBy(o => o.statsChangeType))
        {
            ApplyStatModifier(stat);
        }
    }

    private void ApplyStatModifier(CharacterStat modifier)
    {
        Func<float, float, float> operation = Operation(modifier.statsChangeType);

        UpdateAllStats(operation, modifier);
    }

    private Func<float, float, float> Operation(StatsChangeType statsChangeType)
    {
        return statsChangeType switch
        {
            StatsChangeType.Add => (current, change) => current + change,
            StatsChangeType.Multiple => (current, change) => current * change,
            StatsChangeType.Override => (current, change) => change
        };
    }

    private void UpdateAllStats(Func<float, float, float> operation, CharacterStat modifier)
    {
        if (CurrentStat.statData == null || modifier.statData == null) return;

        var currentStat = CurrentStat.statData;
        var newStat = modifier.statData;

        currentStat.Atk = Mathf.Max(operation(currentStat.Atk, newStat.Atk), MinAttackDmg);
        currentStat.MaxHealth = Mathf.Max((int)operation(currentStat.MaxHealth, newStat.MaxHealth), MinMaxHealth);
        currentStat.Lv = Mathf.Max((int)operation(currentStat.Lv, newStat.Lv), MinLv);
    }
}
