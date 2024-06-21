using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "BtnData", menuName = "ScriptableObjects/BtnData", order = 1)]
public class BtnData : ScriptableObject
{
    [Header("Button Info")]
    public Sprite btnIcon;
    public string btnName;
    public string btnDescription;
    public string loadSceneName;
    public int styleidx;

    [Header("Result Info")]
    public Sprite resultIcon;
    public string resultName;

    [Header("Stage Info")]
    public string stageName;
    public string stageDescription;

    [Header("Appearance probability")]
    public GameObject prefab;
    public float probability;
}
