using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    [Header("PlayerInfo")]
    public int playerLv;
    public float playerAtk;
    public int playerMaxHealth;
}
