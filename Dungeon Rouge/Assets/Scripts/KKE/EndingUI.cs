using System.Collections;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingUI : MonoBehaviour
{
    public Text endUI;
    
    string message = $"ÆÀÀå : À±Ä¡ÈÆ ÆÀ¿ø : ±è°­Àº ÆÀ¿ø : ÀÌÁ¤È£ ÆÀ¿ø : ÇÏÇö¿ì";

    private void Start()
    {
        StartCoroutine(TypeText(message));
        Invoke("LoadScene", 10f);
    }


    IEnumerator TypeText(string message)
    {
        endUI.text = "";
        foreach (char letter in message.ToCharArray())
        {
            endUI.text += letter;
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(0);
    }
}