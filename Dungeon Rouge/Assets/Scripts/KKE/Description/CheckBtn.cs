using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckBtn : MonoBehaviour
{
    public GameObject stamp;
    public string sceneName;

    public void OnCheckBtn()
    {
        stamp.SetActive(true);

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
