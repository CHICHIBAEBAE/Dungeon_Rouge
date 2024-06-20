using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemData", order = 3)]
public class ItemData : ScriptableObject
{
    [Header("ItemInfo")]
    public Image ItemImg;
    public int ItemLv;
    public string ItemName;
    public string ItemInfo;
    public float ItemAtk;
    public float ItemHealth;
    public int ItemGold;
}