using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{    
    public Text[] ItemGold=new Text[5];
    public Image[] ItemImg=new Image[5];
    public Text ItemName;
    public Text ItemInfo; 
    public Text playerHaveGoldTxt;     
    public List<ItemData> ItemList;
    int[] r= new int[4]; 
    public CharacterStat ItemStats; 
    public GameObject player; 
    CharacterStatHandler PlayerModifiders;       
    public GameObject stamp;
    public GameObject loadScene;

    int curGold;
    int curHp;
    int maxHP;

    bool isPay;
    
    void Start() 
    {   
        OnRerole(0);
        PlayerModifiders = player.GetComponent<CharacterStatHandler>();
        //curHp=PlayerModifiders.baseStats.statData.CurHealth;
        maxHP=PlayerModifiders.baseStats.statData.MaxHealth;
        curGold=PlayerModifiders.CurrentStat.statData.PlayerHaveGold;
    }
    void ShowDisplay()
    {   
        
        playerHaveGoldTxt.text= curGold.ToString();
        for (int i = 0; i < 4;i++)
        {
            ItemImg[i].sprite=ItemList[r[i]].ItemImg;
            ItemGold[i].text=ItemList[r[i]].ItemGold.ToString();
        }
    }
    public void OnSelect(int btnNum)
    {           
        ItemName.text=ItemList[r[btnNum]].ItemName.ToString();
        ItemInfo.text=ItemList[r[btnNum]].ItemInfo.ToString();
        ItemGold[4].text=ItemList[r[btnNum]].ItemGold.ToString();
        ItemImg[4].sprite=ItemList[r[btnNum]].ItemImg;

        ItemStats.statData=ItemList[r[btnNum]];
    }   

    public void OnHeal(int gold)
    {        
        // Payment(gold);
        // if(isPay)
        // {       
                 Debug.Log("회복");
                 curHp=maxHP;
        //       isPay=!isPay;
        // }
    }

    public void OnBuyItem()
    {        
        // Payment(ItemStats.statData.ItemGold);
        // if(isPay)
        // {
                Debug.Log("구매");
                PlayerModifiders.AddStatModifier(ItemStats);
                stamp.SetActive(true);
                playerHaveGoldTxt.text= curGold.ToString();
        //     isPay=!isPay;
        // }
    }

    public void OnRerole(int gold)
    {   
        Payment(gold);
        if(isPay)
        {
        RandomValue(r,0,ItemList.Count);
        ShowDisplay();
        isPay=!isPay;
        }
    }

    void RandomValue(int[] _r,int min,int max)
    {
        List<int> numList=new List<int>();
        for(int i=min; i<max;i++) numList.Add(i); //Make >> numList= (0,1,2,3,4)

        for(int i=0; i<_r.Length;i++)
        {
            int ran= Random.Range(0,numList.Count);
            _r[i]=numList[ran]; //use numList(3)
            numList.RemoveAt(ran);//Remove umList(3) ==> numList(0,1,2,4)
        }
    }

    bool Payment(int _gold)
    {
        //if(curGold>=_gold)
        //{
             curGold-=_gold;
             return isPay=true;
        //}               
        //else 
        //{
        //     Debug.Log("님돈없");
        //     return isPay=false;
        //}
    }
}
