using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{    
    public Text[] ItemGold=new Text[5];
    public Image[] ItemImg=new Image[5];
    public Text ItemName;
    public Text ItemInfo; 
    public List<ItemData> ItemList;    
    int[] r= new int[4];      
    
    void Start() 
    {   
        OnRerole();
    }
    void ShowDisplay()
    {        
        for (int i = 0; i < 4;i++)
        {
            //ItemImg[i]=ItemList[r[i]].ItemImg;
            ItemGold[i].text=ItemList[r[i]].ItemGold.ToString();
        }
    }
    public void OnSelect(int btnNum)
    {                
        //ItemImg[i]=ItemList[btnNum].ItemImg;
        ItemName.text=ItemList[btnNum].ItemName.ToString();
        ItemInfo.text=ItemList[btnNum].ItemInfo.ToString();
        ItemGold[4].text=ItemList[btnNum].ItemGold.ToString();               
    }

    public void OnHeal()
    {

    }

    public void OnRerole()
    {         
        RandomValue(r,0,ItemList.Count);        
        ShowDisplay();
        Debug.Log($"{r[0]}, {r[1]}, {r[2]}, {r[3]}");
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
}