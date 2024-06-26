using System.Collections;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingUI : MonoBehaviour
{
    public Text endUI;
    
    string message = $"팀장 : 윤치훈 팀원 : 김강은 팀원 : 이정호 팀원 : 하현우";

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
        DataManager.instance.styleIdx =1;
        BGMManager.BGMinstance.BGMStop();        
        SceneManager.LoadScene(0);
    }
}