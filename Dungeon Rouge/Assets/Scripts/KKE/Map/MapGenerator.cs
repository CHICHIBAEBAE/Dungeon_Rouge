using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{
    public List<BtnData> prefabDataList;
    public Transform pointSpot;

    private void Start()
    {
        Debug.Log($"{MapManager.instance.MapDataList.Count}");
        
        if (MapManager.instance.MapDataList.Count > 0)
        {
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

                MapManager.instance.MapDataList.Add(mapData);
            }
        }
    }

    private void LoadMap()
    {
        foreach (MapData mapData in MapManager.instance.MapDataList)
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

        //��� �������� Ȯ�� �ջ��Ͽ� ��ü Ȯ�� ���
        foreach (BtnData data in prefabDataList)
        {
            totalProbability += data.probability;
        }

        //Random.value ���� 0 ~ 1 ������ ���� ��
        //��ü Ȯ���� Random.value �� ���Ͽ� ���� ����Ʈ ���� ( 0 < randomPoint < totalProbability )
        float randomPoint = Random.value * totalProbability;
        float currentProbability = 0f;

        //�����տ� ������ Ȯ���� currentProbability�� �ջ��� randomPoint ���� ���� ��� ����
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
