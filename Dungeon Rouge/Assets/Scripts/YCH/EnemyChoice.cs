using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChoice : MonoBehaviour
{
    int choice;

    public GameObject normalEnemy;
    public GameObject eliteEnemy;
    public GameObject bossEnemy;
    public GameObject normalBG;
    public GameObject eliteBG;
    public GameObject bossBG;

    void Awake()
    {
        choice = DataManager.instance.styleIdx;

        switch (choice)
        {
            case 0:
                normalEnemy.SetActive(true);
                eliteEnemy.SetActive(false);
                bossEnemy.SetActive(false);
                normalBG.SetActive(true);
                eliteBG.SetActive(false);
                bossBG.SetActive(false);
                return;
            case 1:
                normalEnemy.SetActive(false);
                eliteEnemy.SetActive(true);
                bossEnemy.SetActive(false);
                normalBG.SetActive(false);
                eliteBG.SetActive(true);
                bossBG.SetActive(false);
                return;
            case 2:
                BGMManager.BGMinstance.BGMStop();
                normalEnemy.SetActive(false);
                eliteEnemy.SetActive(false);
                bossEnemy.SetActive(true);
                normalBG.SetActive(false);
                eliteBG.SetActive(false);
                bossBG.SetActive(true);
                return;
        }
    }
}
