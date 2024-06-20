using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] StatData playerData;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnAttack()
    {
        Debug.Log($"{playerData.Atk}!!!");
    }
}
