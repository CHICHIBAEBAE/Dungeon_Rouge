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

    void Awake()
    {
        choice = DataManager.instance.styleIdx;

        switch (choice)
        {
            case 0:
                normalEnemy.SetActive(true);
                eliteEnemy.SetActive(false);
                bossEnemy.SetActive(false);
                return;
            case 1:
                normalEnemy.SetActive(false);
                eliteEnemy.SetActive(true);
                bossEnemy.SetActive(false);
                return;
            case 2:
                normalEnemy.SetActive(false);
                eliteEnemy.SetActive(false);
                bossEnemy.SetActive(true);
                return;
        }
    }
}
