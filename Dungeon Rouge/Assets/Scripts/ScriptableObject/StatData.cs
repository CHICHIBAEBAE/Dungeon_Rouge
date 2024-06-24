using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatData", menuName = "ScriptableObjects/StatData", order = 1)]
public class StatData : ScriptableObject
{
    [Header("Stat")]
    public float Atk;
    public int CurHealth;
    public int MaxHealth;
    public int PlayerHaveGold;
}
