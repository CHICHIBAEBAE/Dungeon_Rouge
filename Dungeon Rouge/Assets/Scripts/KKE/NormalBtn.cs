using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBtn : MonoBehaviour
{
    public GameObject description;

    public void OnNormalBtn()
    {
        description.SetActive(true);
    }
}
