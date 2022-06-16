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

    public List<SuggestedIndex> CountOfSuggestion = new List<SuggestedIndex>();
    public SuggestedIndex suggestedIndex = new SuggestedIndex();

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
    public int GetCodeOfTable(string name) //50001
    {
        int index = int.Parse(Regex.Replace(name, @"\D", ""));
        if (index < 10)
        {
            return CodeNum + index;
        }
        else
        {
            return (CodeNum/10) + index;
        }
    }

    public int x;

    public bool CanFind(string WordCode)
    {
        foreach (var item in CountOfSuggestion)
        {
            if (item.TableID == Convert.ToInt32((GetCodeOfTable(WordCode))))
            {
                item.Count++;
                x = item.Count;
                return true;
            }
        }
        return false;
    }
    public int GetNewWordIndex(string WordCode) // new
    {

        if(!CanFind(WordCode))
        {
            suggestedIndex = new SuggestedIndex();
            suggestedIndex.TableID = Convert.ToInt32((GetCodeOfTable(WordCode)));
            suggestedIndex.Count = 1;
            CountOfSuggestion.Add(suggestedIndex);
            x = 1;
        }


        for (int i = 0; i < Tables.Count; i++)
        {
            if(Tables[i].TableNameCode.Contains( WordCode))
            {
                return (Convert.ToInt32((GetCodeOfTable(WordCode)*100)) + (Tables[i].Word.Count+x)); //50001 * 100 * 100 + 05 = 5000105
            }
        }
        return 0;
    }
    public int GetWordID(string WordCode,int index) //5000101
    {
        for (int i = 0; i < Tables.Count; i++)
        {
            if (Tables[i].TableNameCode.Contains(WordCode))
            {
                return (Convert.ToInt32((GetCodeOfTable(WordCode) * 100)) + index); //50001 * 100  + 05 = 5000105
            }
        }
        return 0;
    }

    public string GetNameOfTableByCode(string WgCode) //پیش فرض
    {
        for (int i = 0; i < TablesName.Count ; i++)
        {
            if (TablesName[i].NameCode == WgCode)
                return TablesName[i].Name;
        }
        return null;
    }
   
    public string GetWordGroupCode(int index) // make wg-01
    {
        if (index < 10)
        {
            return "wg-0" + index;
        }
        else
        {
            return "wg-" + index;
        }
    }
    

    string WordCode; 
    public void RemoveWord(int index,int ListIndex)
    {
        GameObject tmp =  SingeltonManager.Instance.publicWordPool.pooledObjects[ListIndex].wordGroup[index];
        WordCode = GetWordGroupCode(ListIndex+1);
        SingeltonManager.Instance.wordGroupControler.RemoveWord(GetWordID(WordCode,index+1), WordCode);
        tmp.SetActive(false);
        
    }
    public void GetAllPublicTable(ref List<string> Items)
    {
        foreach (var item in SingeltonManager.Instance.wordGroupControler.TablesNameFromDataBase)
        {
            Items.Add(item.Name);
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

[Serializable]
public class SuggestedWord
{
    public string Word; //کلمه پیشنهادی 
    public string WgName; //wg-01
    public int WgCode; //5000101
}

[Serializable]
public class SuggestedIndex
{
    public int TableID;
    public int Count;
}
