using System;
using System.Collections.Generic;
using UnityEngine;

public class PublicWordsManager : MonoBehaviour
{
    public int BtnCount = 20;
    public int WordCount = 80;

    
    public List<TableDetail> Tables
    {
        get
        {
            return SingeltonManager.Instance.wordGroupControler.Tables;
        }
       
    }
}


[Serializable] 
public class TableDetail
{
    public string NameCode;
    public string Name;
    public int ID;
}