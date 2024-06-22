using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventData", menuName = "ScriptableObjects/EventData", order = 1)]
public class EventData : ScriptableObject
{      
    [Header("EventInfo")]
    
    public string EventName;
    public string EventInfo;
    public string EventChoice1;    
    public string EventChoice2;    

    [Header("EventChoice1")]
    public List<EventStat> eventModifiers1 = new List<EventStat>();

    [Header("EventChoice2")]
    public bool EventOnChoice2;
    public List<EventStat> eventModifiers2 = new List<EventStat>();

    [Header("EventChoice3")]
    public bool EventOnChoice3;
}
