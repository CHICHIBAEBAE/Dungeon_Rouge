using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemData", order = 3)]
public class ItemData : StatData
{
    [Header("ItemInfo")]

    public Sprite ItemImg;
    public string ItemName;
    public string ItemInfo;
    public int ItemGold;
}