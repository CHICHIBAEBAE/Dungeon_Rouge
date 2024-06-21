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
        Transform[] generatorSpots = pointSpot.GetComponentsInChildren<Transform>();

        foreach (Transform generatorSpot in generatorSpots)
        {
            if (generatorSpot != pointSpot)
            {
                GameObject selectedPrefab = GetRandomPrefab();
                Instantiate(selectedPrefab, generatorSpot.position, generatorSpot.rotation, generatorSpot);
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
