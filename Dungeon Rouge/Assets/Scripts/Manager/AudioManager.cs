using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider audioSlider;
    public static AudioManager instance;
    public Text onOffTxt;
    bool onOff=true;
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
    }

    public void AudioControl()
    {
        float sound = audioSlider.value;        

        if (sound == -40f)
            audioMixer.SetFloat("BGM", -80f);
        else 
            audioMixer.SetFloat("BGM", sound);
    }

    public void OnAudioTogle()
    {  
        onOff=!onOff;        
        if(onOff)
        {   
            onOffTxt.text="Off";
            audioMixer.SetFloat("BGM", 0f);
        }        
        else
        {   
            onOffTxt.text="On";
            audioMixer.SetFloat("BGM", 1f);
        }
    }    
}
