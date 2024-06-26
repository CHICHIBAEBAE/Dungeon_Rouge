using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RandomUI : MonoBehaviour
{
    public GameObject loadScene;
    public GameObject ranPanel;
    public GameObject Description;
    public Text playerHaveGoldTxt;
    public Text OutputTxt;
    public Text EventName;
    public Text EventInfo;
    public Text Choice1;
    public Text Choice2;
    public GameObject Choice2Btn;
    public GameObject Choice3Btn;
    public GameObject player;
    public GameObject shop;
    ShopUI shopListStats;
    List<ItemData> shopList;
    public CharacterStat ItemStats; 
    CharacterStatHandler characterStatHandler;    
    public List<EventData> eventList=new List<EventData>();
    int random;
    int curGold;
    int curHp;    
    bool isMoveScene=false;
    
    void Start()
    {   
        
        player = Player.instance.gameObject;
        characterStatHandler = player.GetComponent<CharacterStatHandler>();
        shopListStats=shop.GetComponent<ShopUI>();
        shopList=shopListStats.ItemList;
        random= Random.Range(0, eventList.Count);               
        curGold = Player.instance.curMoney;
        playerHaveGoldTxt.text = $"{curGold}";        

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
        OnEvent(eventList[random],btnNum);
    }
    void OnEvent(EventData eventData,int _btnNum)
    {   
        if(_btnNum==0)
        {            
            foreach (EventStat stat in eventData.eventModifiers1.OrderBy(o => o.eventType))
            {   
                StartEventModifier(stat);
            }           
        }
        else if(_btnNum==1)
        {               
            foreach (EventStat stat in eventData.eventModifiers2.OrderBy(o => o.eventType))
            {
                StartEventModifier(stat);
            }            
        }
        else return;
    }   
    void StartEventModifier(EventStat stat)
    {        
        int percent=Random.Range(0,10);        
        if(percent<=10*stat.percent && !isMoveScene)
        { 
            switch(stat.eventType)
            {
                case EventType.GiveGold:                    
                        //Debug.Log("골드획득 "+ stat.intNum);
                        Player.instance.curMoney +=stat.intNum;
                        playerHaveGoldTxt.text = $"{Player.instance.curMoney}";
                        OutputTxt.text=$"{stat.intNum} 원 획득";  
                break;
                case EventType.GiveMaxHealth:                    
                        //Debug.Log("체력회복 "+ stat.intNum);
                        curHp=Player.instance.curHP;
                        Player.instance.curHP +=stat.intNum;                        
                        OutputTxt.text=$"{curHp}HP {stat.intNum}= {Player.instance.curHP}HP";
                break;
                case EventType.GiveLv:                    
                        Debug.Log("렙업 : "+ stat.intNum);
                        //characterStatHandler.CurrentStat.statData.Lv; +=stat.intNum;                    
                break;
                case EventType.GiveItem:      
                        int ran=Random.Range(0,shopList.Count);
                        ItemStats.statData=shopList[ran];
                        characterStatHandler.AddStatModifier(ItemStats); 
                        OutputTxt.text=$"획득:{shopList[ran].ItemName}";
                break;
                default:
                  isMoveScene=true;
                  MoveScene(stat.intNum);
                break;
            }
        }
    }
    void MoveScene(int num)
    {
        DataManager.instance.styleIdx=num;
        if(num==1||num==2)
        {
            OutputTxt.text="전장 이동";
            loadScene.SetActive(true);
            StartCoroutine(SceneMoveTime(2));
        }
        else if(num==3)
        {
             OutputTxt.text="상점 이동";
            loadScene.SetActive(true);
            StartCoroutine(SceneMoveTime(3));
        }
        else
        {
            //OutputTxt.text="맵으로 이동";                       
            loadScene.SetActive(true);
            StartCoroutine(SceneMoveTime(1));
        }
    }
    IEnumerator SceneMoveTime(int num)
    {           
        yield return new WaitForSeconds(1.5f);
        isMoveScene=false;        
        SceneManager.LoadScene(num);
    }
}
