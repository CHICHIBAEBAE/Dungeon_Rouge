using UnityEngine;

public class ShopBtn : MonoBehaviour
{
    public GameObject description;

    public Animator anim;
    public string animName;    

    public void OnLogBtn()
    {        
        description.SetActive(true);
        anim.Play(animName);
    }
}
