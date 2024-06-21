using UnityEngine;

[CreateAssetMenu(fileName = "EventData", menuName = "ScriptableObjects/EventData", order = 4)]
public class EventData : StatData
{  
    [Header("EventInfo")]
    
    public string EventName;
    public string EventInfo;
    public string EventChoice1;
    public bool EventOnChoice2;
    public string EventChoice2;
    public bool EventOnChoice3;

    [Header("EventReward")]
    public float percent;
    public int ItemGold;
    public int styleidx;
}
