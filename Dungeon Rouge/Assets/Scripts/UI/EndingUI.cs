using System.Collections;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingUI : MonoBehaviour
{
    public Text endUI;
    
    string message = $"���� : ��ġ�� ���� : �谭�� ���� : ����ȣ ���� : ������";

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