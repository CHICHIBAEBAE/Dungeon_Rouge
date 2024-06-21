﻿using UnityEngine;

public class SkillTest : MonoBehaviour
{
    public CharacterStatHandler statHandler;

    private void Start()
    {
        Character enemy = new Character("Enemy", 100);

        statHandler = GetComponent<CharacterStatHandler>();

        Skill powerStrike = new Skill(
            "파워 스트라이크",
            statHandler.CurrentStat.statData.Atk * 1.2f,
            enemy => enemy.TakeDamage(statHandler.CurrentStat.statData.Atk * 1.2f)
            );

        powerStrike.Execute(enemy);
        Debug.Log("빠워스뜨라이크");
    }
}
