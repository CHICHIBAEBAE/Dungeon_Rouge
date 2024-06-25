using UnityEngine;
using UnityEngine.UI;

public class CheatBtn : MonoBehaviour
{
    public Transform pointSpot;
    public Button endBtn;

    public void OnClickCheatBtn()
    {
        foreach (Transform child in pointSpot.transform)
        {
            BtnController btnController = child.GetComponentInChildren<BtnController>();

            if (btnController != null)
            {
                Button button = btnController.GetComponent<Button>();

                if (button != null)
                {
                    button.interactable = true;
                    endBtn.interactable = true;
                }
            }
        }
    }
}