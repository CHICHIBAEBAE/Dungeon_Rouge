using UnityEngine;

public class Player : MonoBehaviour
{
    static public Player instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int curMoney;
    public int curHP;
    
}
