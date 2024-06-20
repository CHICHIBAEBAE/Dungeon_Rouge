using UnityEngine;
using UnityEngine.UI;

public class DescriptionManager : MonoBehaviour
{
    public Image btnIcon;
    public Image resultIcon;
    public Text btnName;
    public Text resultTxt;
    public Text stageName;
    public Text stageDescription;

    public void SetDescription(BtnData btnData)
    {
        btnIcon.gameObject.SetActive(true);
        resultIcon.gameObject.SetActive(true);

        btnIcon.sprite = btnData.btnIcon;
        resultIcon.sprite = btnData.resultIcon;

        btnName.text = btnData.btnName;
        resultTxt.text = btnData.resultName;

        stageName.text = btnData.stageName;
        stageDescription.text = btnData.stageDescription;
        
    }
}