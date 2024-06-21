using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventSceneUI : MonoBehaviour
{
    int choice;
    public GameObject ShopPanel;  // Shop
    public GameObject RandomPanel; //Random Event
    
    void Start() 
    {   
        choice=DataManager.instance.styleIdx;

        switch(choice)
        {
            case 3:
                ShopPanel.SetActive(true);
                RandomPanel.SetActive(false);                
            return;
            case 4:
                ShopPanel.SetActive(false);
                RandomPanel.SetActive(true);                
            return;
            default:
            return;
        }
    }    
    public void OnEventExit()
    {
        StartCoroutine(EventExit());
    }
    IEnumerator EventExit()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(1);
        DataManager.instance.styleIdx=0;
    }
}
