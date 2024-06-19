using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerData playerData;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnAttack()
    {
        Debug.Log($"{playerData.playerAtk}!!!");
    }
}
