using System;

public enum EventType
{
    GiveGold,
    GiveMaxHealth,
    GiveLv,
    GiveItem,
    MoveScene    
}

[Serializable]
public class EventStat
{
    public EventType eventType;    
    public float percent; 
    public int intNum;    
}