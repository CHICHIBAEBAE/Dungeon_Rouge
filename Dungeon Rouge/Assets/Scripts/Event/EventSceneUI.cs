using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventSceneUI : MonoBehaviour
{
    int choice;
    public GameObject ShopPanel;  // Shop
    public GameObject RandomPanel; //Random Event

    public GameObject loadScene;
    public Animator anim;
    public string animName;
    
    void Start() 
    {   
        loadScene.SetActive(true);
        anim.Play(animName);

        StartCoroutine(WaitForAnimation(anim, animName));

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

    public IEnumerator WaitForAnimation(Animator anim, string animName)
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        float animationDuration = stateInfo.length;

        yield return new WaitForSeconds(animationDuration);

        loadScene.SetActive(false);
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
