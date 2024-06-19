using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitBtn : MonoBehaviour
{
    public Animator anim;
    public string animName;
    public Button exitBtn;

    private void Start()
    {
        if (exitBtn != null)
        {
            exitBtn.onClick.AddListener(OnExitBtn);
        }
    }

    public void OnExitBtn()
    {
        anim.Play(animName);
    }
}
