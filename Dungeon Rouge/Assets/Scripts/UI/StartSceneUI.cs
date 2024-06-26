using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneUI : MonoBehaviour
{
    public GameObject OptionPanel;
    public GameObject loadScene;

    public void OnStart()
    {
        loadScene.SetActive(true);

        DataManager.instance.btnCount = 0;
        Invoke("LoadScene", 1.5f);
    }

    public void OnOption()
    {
        OptionPanel.SetActive(true);
    }
    public void OffOption()
    {
        OptionPanel.SetActive(false);
    }

    public void OnAdd()
    {

    }

    public void OnExit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }
}
