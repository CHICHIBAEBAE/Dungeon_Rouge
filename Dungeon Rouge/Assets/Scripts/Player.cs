using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] StatData playerData;
    public CharacterStatHandler characterStatHandler;

    void Start()
    {
        characterStatHandler = GetComponent<CharacterStatHandler>();
    }

    void Update()
    {
        
    }

    public void OnAttack()
    {
        Debug.Log($"{characterStatHandler.CurrentStat.statData.Atk}!!!");
    }
}
