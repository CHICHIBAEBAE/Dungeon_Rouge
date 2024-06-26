using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckBtn : MonoBehaviour
{
    public GameObject stamp;
    public string sceneName;
    public BtnData btnData;

    public void OnCheckBtn()
    {
        DataManager.instance.btnCount++;
        int btnCount = DataManager.instance.btnCount;
        
        stamp.SetActive(true);
        DataManager.instance.styleIdx = btnData.styleidx;

        if (!string.IsNullOrEmpty(sceneName))
        {
            Invoke("LoadScene", 1.5f);
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}