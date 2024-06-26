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
    public GameObject description;

    private bool isPlaying = false;

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
        isPlaying = true;
    }

    private void Update()
    {
        if (isPlaying)
        {
            AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.IsName(animName) && stateInfo.normalizedTime >= 1f)
            {
                description.SetActive(false);
                isPlaying = false;
            }
        }
    }
}
