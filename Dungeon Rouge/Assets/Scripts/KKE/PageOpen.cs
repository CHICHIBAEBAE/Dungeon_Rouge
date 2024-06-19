using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageOpen : MonoBehaviour
{
    public GameObject closePage;
    public GameObject openPage;

    private void OpenPage()
    {
        closePage.SetActive(false);
        openPage.SetActive(true);
    }
}
