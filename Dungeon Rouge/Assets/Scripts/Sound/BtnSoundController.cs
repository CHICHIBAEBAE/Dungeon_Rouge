using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnSoundController : MonoBehaviour
{
    public GameObject audioManager;
    public AudioManager audioSorce;
    public Button Btn;
    public int num;

    void Start()
    {
        audioManager=AudioManager.instance.gameObject;
        audioSorce =audioManager.GetComponent<AudioManager>();
        Btn=this.GetComponent<Button>();
        Btn.onClick.AddListener(OnSoundBtn);
    }
    public void OnSoundBtn()
    {
        audioSorce.OnSoundBtn(num);
    }
}
