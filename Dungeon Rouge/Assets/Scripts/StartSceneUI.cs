using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneUI : MonoBehaviour
{
    public GameObject OptionPanel;
    public void OnStart()
    {
        SceneManager.LoadScene(1);
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

}
