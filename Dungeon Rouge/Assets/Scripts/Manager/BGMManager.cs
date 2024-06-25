using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class BGMManager : MonoBehaviour
{
    public AudioMixer audioMixer; 
    public Slider BGMSlider;
    public static BGMManager BGMinstance;
    AudioSource BGMSource;    
    public List<AudioClip> clipList=new List<AudioClip>();
    public AudioClip BossClip;
    public AudioClip EndingClip;
    
   
    private void Awake()
    {
        if(BGMinstance == null)
        {
            BGMinstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        BGMSource=GetComponent<AudioSource>();                
    }
    void Start()    
    {
        audioMixer.SetFloat("BGM", -20f);
    }
    void Update()
    {
        if(DataManager.instance.styleIdx==2)
        {   
            BGMSource.Stop();          
            BGMSource.clip=BossClip;
        }
        else if (DataManager.instance.styleIdx==9)
        {      
            BGMSource.Stop();
            BGMSource.clip=EndingClip; 
        }
        else if (!BGMSource.isPlaying)
        {
            BGMSource.clip = clipList[Random.Range(0, clipList.Count)];           
        }        
        BGMSource.Play();
    }

    public void BGMAudioControl()
    {
        float sound = BGMSlider.value;

        if (sound == -40f)
            audioMixer.SetFloat("BGM", -80f);
        else 
            audioMixer.SetFloat("BGM", sound);
    }
}
