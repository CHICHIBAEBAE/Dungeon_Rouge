using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamp : MonoBehaviour
{
    public GameObject stampMark;

    public GameObject loadScene;

    private void ActivateObject()
    {
        if (stampMark != null)
        {
            stampMark.SetActive(true);
            loadScene.SetActive(true);
        }
    }
}
