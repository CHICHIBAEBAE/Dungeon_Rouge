using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalBtn : MonoBehaviour
{
    public GameObject description;

    public Animator anim;
    public string animName;
    public Button normalBtn;

    private void Start()
    {
        if (description == null)
        {
            description = GameObject.Find("Description_UI").transform.Find("Description").gameObject;
        }

        if (anim == null && description != null)
        {
            anim = description.GetComponent<Animator>();
        }
        
        if (normalBtn != null)
        {
            normalBtn.onClick.AddListener(OnNormalBtn);
        }
    }

    public void OnNormalBtn()
    {
        description.SetActive(true);
        anim.Play(animName);
    }
}
