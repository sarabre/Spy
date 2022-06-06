using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class PublicWordsManager : MonoBehaviour
{
    public int BtnCount = 20;
    public int WordCount = 80;
    public int CodeNum = 50000;

    public TableDetail tableDetail = new TableDetail();
    public Table table = new Table();

    private List<TableDetail> TableNamesFromDatabase
    {
        get
        {
            return SingeltonManager.Instance.wordGroupControler.TablesNameFromDataBase;
        }

    }

    public List<TableDetail> TablesName = new List<TableDetail>();


    public List<Table> Tables = new List<Table>();
    public List<Table> TablesFromDataBase
    {
        get
        {
            return SingeltonManager.Instance.wordGroupControler.Tables;
        }
    }



    public void SortTable()
    {
        //  To match IDs of LiSBtn GameObject(IDGenerator data does not change) to IDs in the database,
        //  we add every 20 TableDatail to an array. However, we do not quantity the ID.
        //  We check IDs in PoolManager for active objects.

        for (int i = TablesName.Count; i < BtnCount; i++)
        {
            TablesName.Add(tableDetail);
            tableDetail = new TableDetail();
        }

        foreach (var item in TableNamesFromDatabase)
        {
            TablesName.Insert(item.ID - (CodeNum+1), item);
        }
        
    }

    public void SortWords()
    {
        
        for (int i = Tables.Count; i < BtnCount; i++)
        {
            Tables.Add(table);
            table = new Table();
        }

        foreach (var item in TablesFromDataBase)
        {
            try
            {
                int index = int.Parse(Regex.Replace(item.TableNameCode, @"\D", ""));
                Tables.Insert(index - 1, item);
            }
            catch
            {

            }
            
        }

    }
    public int GetCodeOfTable(string name)
    {
        return CodeNum + int.Parse(Regex.Replace(name, @"\D", ""));
    }

    public int GetNewWordIndex(string WordCode)
    {
        for (int i = 0; i < Tables.Count; i++)
        {
            if(Tables[i].TableNameCode.Contains( WordCode))
            {
                return Tables[i].Word.Count;
            }
        }
        return 0;
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

[Serializable]
public class SuggestedWord
{
    public string Word; //کلمه پیشنهادی 
    public string WgName; //wg-01
    public int WgCode; //5000101
}