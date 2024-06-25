using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemLIstView : BattleController
{    
    public List<Image>[] ItemImg = new List<Image>[6];
    List<StatData>[] itemData=new List<StatData>[6];    

    void Start() 
    {           

        for (int i=0;i <ItemImg.Length; i++)
        {
            if(itemData[i]!=null)
            {
                //itemData[i]= characterStatHandler.
                //ItemImg[i].sprite=itemData[i].ItemImg;
            }
        }        
    }
}
