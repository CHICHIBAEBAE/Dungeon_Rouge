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
    Artifact artifact;
    ArtifactModifiers ItemMod;
    public GameObject player;    
    
    void Start() 
    {   
        OnRerole();        
        artifact = GetComponent<Artifact>();
        ItemMod=GetComponent<ArtifactModifiers>();
    }
    void ShowDisplay()
    {        
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
        
    }
    

    public void OnHeal()
    {

    }

    public void BayItem()
    {
        Debug.Log("구매");
        //Player.stat.gold-=ItemList[r[btnNum]].ItemGold;
        artifact.UseArtifact(player);
    }

    public void OnRerole()
    {         
        RandomValue(r,0,ItemList.Count);        
        ShowDisplay();        
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
