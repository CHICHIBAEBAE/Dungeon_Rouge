using UnityEngine;

public class SoundTest : MonoBehaviour
{
    public AudioClip EffectSound;
    AudioSource audioSource;
    
    void Start()
    {        
        audioSource = AudioManager.instance.GetComponent<AudioSource>();
    }
    
    public void OnSoundTestBtn()
    {        
        audioSource.PlayOneShot(EffectSound);
    }
}
