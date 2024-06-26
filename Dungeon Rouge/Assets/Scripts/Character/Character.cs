
using UnityEngine;

public class Character : MonoBehaviour
{
    public string Name;
    public float Health;

    public Character(string name, float health)
    {
        Name = name;
        Health = health;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        Debug.Log($"{Name}의 피가{damage}만큼 까였다");
    }
}