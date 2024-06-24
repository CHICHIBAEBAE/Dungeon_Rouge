using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public int mapIdx;
    public int styleIdx; //null:0 BattleScene:1,2 EventScene:Shop=3,Random=4
    public int btnCount = 0;
    public List<MapData> MapDataList { get; set; } = new List<MapData>();

    private void Awake()
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
    
}
