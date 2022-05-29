using System;
using System.Collections.Generic;
using UnityEngine;

public class PublicWordsManager : MonoBehaviour
{
    public int BtnCount = 20;
    public int WordCount = 80;

    
    public List<TableDetail> TablesName
    {
        get
        {
            return SingeltonManager.Instance.wordGroupControler.TablesName;
        }
       
    }

    public List<Table> Tables
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
    public string NameCode; //wg-01
    public string Name; //پیش فرض
    public int ID; //50001
}

[Serializable]
public class Table
{
    public List<WordDetail> Word = new List<WordDetail>(); // کلمه ۱ و 5000101
    public string TableNameCode; //wg-01
}

[Serializable]
public class WordDetail
{
    public string Word; // کلمه ۱
    public int ID; //5000101
}