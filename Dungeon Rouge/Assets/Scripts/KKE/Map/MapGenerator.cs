using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject contentPrefabs;

    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {

        }
        
        GameObject map = Instantiate(contentPrefabs);
        map.transform.SetParent(transform);
    }
}
