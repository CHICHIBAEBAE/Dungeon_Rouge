using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnController : MonoBehaviour
{
    public GameObject description;

    public Animator anim;
    public string animName;
    public Button btn;

    public BtnData btnData;
    private DescriptionManager descriptionManager;

    private void Start()
    {
        descriptionManager = FindObjectOfType<DescriptionManager>();

        if (description == null)
        {
            description = GameObject.Find("Description_UI").transform.Find("Description").gameObject;
        }

        if (anim == null && description != null)
        {
            anim = description.GetComponent<Animator>();
        }
        
        if (btn != null)
        {
            btn.onClick.AddListener(OnClickBtn);
        }
    }

    public void OnClickBtn()
    {
        descriptionManager.SetDescription(btnData);
        description.SetActive(true);
        anim.Play(animName);

        CheckBtn checkBtn = FindObjectOfType<CheckBtn>();

        if (checkBtn != null)
        {
            checkBtn.sceneName = btnData.loadSceneName;
            checkBtn.btnData = btnData;
        }
    }   
}
