using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBtn : MonoBehaviour
{
    public GameObject stamp;

    public void OnCheckBtn()
    {
        stamp.SetActive(true);
    }
}
