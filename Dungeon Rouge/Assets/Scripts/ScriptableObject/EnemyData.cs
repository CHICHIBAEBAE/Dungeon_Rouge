using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyData", menuName = "SriptableObjects/EnemyData", order = 2)]
public class EnemyData : ScriptableObject
{
    [Header("EnemyInfo")]
    public int enemyInfo;
    public float enemyAtk;
    public float enemyHealth;
}

