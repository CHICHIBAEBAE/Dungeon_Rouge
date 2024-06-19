using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamp : MonoBehaviour
{
    public GameObject stampMark;

    private void ActivateObject()
    {
        if (stampMark != null)
        {
            stampMark.SetActive(true);
        }
    }
}
