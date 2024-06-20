using UnityEngine;
using UnityEngine.UI;

public class BtnManager : MonoBehaviour
{
    public BtnData btnData;

    public Image icon;
    public Text btnName;

    private void Awake()
    {
        Set();
    }

    public void Set()
    {
        icon.gameObject.SetActive(true);
        icon.sprite = btnData.btnIcon;
        btnName.text = btnData.btnName;
    }
}