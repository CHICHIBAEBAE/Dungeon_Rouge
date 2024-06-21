using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RandomUI : MonoBehaviour
{
    public GameObject ranPanel;
    public GameObject Description;

    public Text EventName;
    public Text EventInfo;
    public Text Choice1;
    public Text Choice2;
    public GameObject Choice2Btn;
    public GameObject Choice3Btn;
    public List<EventData> eventList=new List<EventData>();
    int random;
    
    void Start()
    {   
        random= Random.Range(0, eventList.Count);

        if(ranPanel.activeSelf==true)
        {
            Description.SetActive(true);
            if(eventList[random].EventOnChoice2) Choice2Btn.SetActive(true);
            else Choice2Btn.SetActive(false);
            if(eventList[random].EventOnChoice3) Choice3Btn.SetActive(true);
            else Choice3Btn.SetActive(false);
        }

        EventName.text=eventList[random].EventName;
        EventInfo.text=eventList[random].EventInfo;
        Choice1.text=eventList[random].EventChoice1;
        Choice2.text=eventList[random].EventChoice2;
    }

    public void OnChoice(int btnNum)
    {
        switch(btnNum)
        {
            case 0:            
               OnEvent(eventList[random]);
            return;

            case 1:

            return;

            default:

            return;
        }
    }
    void OnEvent(EventData eventData)
    {

    }
}
