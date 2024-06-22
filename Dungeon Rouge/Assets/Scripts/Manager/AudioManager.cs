using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;     
    public Slider effectSlider;
    public static AudioManager instance;
    public Text onOffTxt;
    AudioSource EffectSource;
    
    public List<AudioClip> clipList=new List<AudioClip>();
   
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        EffectSource=GetComponent<AudioSource>();
    }

    public void ToggleVolume()
    {
        if(AudioListener.volume==0)
        {
            AudioListener.volume= 1;
            onOffTxt.text="ON";
        }
        else
        {
            AudioListener.volume= 0;
            onOffTxt.text="Off";
        }
    }
    
    public void EffectAudioControl()
    {
        float sound = effectSlider.value;

        if (sound == -40f)
            audioMixer.SetFloat("SFX", -80f);
        else 
            audioMixer.SetFloat("SFX", sound);
    }

    public void OnSoundBtn(int clipNum)
    {        
        EffectSource.PlayOneShot(clipList[clipNum]);
    }
}
