using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{
    public List<BtnData> prefabDataList;
    public Transform pointSpot;

    private void Start()
    {   
        if (DataManager.instance.MapDataList.Count > 0)
        {
            if (pointSpot != null)
            {
                foreach (Transform child in pointSpot.transform)
                {
                    child.gameObject.SetActive(false);
                }
            }
            
            LoadMap();
        }
        else
        {
            GenerateMap();
        }
    }

    private void GenerateMap()
    {
        Transform[] generatorSpots = pointSpot.GetComponentsInChildren<Transform>();

        foreach (Transform generatorSpot in generatorSpots)
        {
            if (generatorSpot != pointSpot)
            {
                GameObject selectedPrefab = GetRandomPrefab();
                Instantiate(selectedPrefab, generatorSpot.position, generatorSpot.rotation, generatorSpot);

                MapData mapData = new MapData
                {
                    position = generatorSpot.position,
                    rotation = generatorSpot.rotation,
                    prefabName = selectedPrefab.name
                };

                DataManager.instance.MapDataList.Add(mapData);
            }
        }
    }

    private void LoadMap()
    {
        foreach (MapData mapData in DataManager.instance.MapDataList)
        {
            BtnData btnData = prefabDataList.Find(data => data.prefab.name == mapData.prefabName);

            if (btnData != null)
            {
                Instantiate(btnData.prefab, mapData.position, mapData.rotation, pointSpot);
            }
        }
    }

    private GameObject GetRandomPrefab()
    {
        float totalProbability = 0f;

        //모든 프리팹의 확률 합산하여 전체 확률 계산
        foreach (BtnData data in prefabDataList)
        {
            totalProbability += data.probability;
        }

        //Random.value 값은 0 ~ 1 사이의 랜덤 값
        //전체 확률과 Random.value 를 곱하여 랜덤 포인트 설정 ( 0 < randomPoint < totalProbability )
        float randomPoint = Random.value * totalProbability;
        float currentProbability = 0f;

        //프리팹에 설정된 확률을 currentProbability에 합산해 randomPoint 보다 작을 경우 생성
        foreach (BtnData data in prefabDataList)
        {
            currentProbability += data.probability;

            if (randomPoint < currentProbability)
            {
                return data.prefab;
            }
        }

        return null;
    }
}
