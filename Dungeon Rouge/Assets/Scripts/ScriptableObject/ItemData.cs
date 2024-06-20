using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemData", order = 3)]
public class ItemData : ScriptableObject
{
    [Header("ItemInfo")]
    public Sprite ItemImg;
    public int ItemLv;
    public string ItemName;
    public string ItemInfo;
    public float ItemAtk;
    public float ItemHealth;
    public int ItemGold;
}