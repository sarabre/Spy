using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class PersonalWordsManager : MonoBehaviour
{
    private List<ListContent> WordsList = new List<ListContent>();

    private ListContent listContentForNewGroup = new ListContent();


    // read-only variable for encapsulation
    public List<ListContent> wordlist
    {
        get
        {
            return WordsList;
        }
    }

    public int BtnCount = 12;
    public int WordCount = 60;

    string serializedJson;

    // should Quantify in RUN time
    public void Awake()
    {
        if (PlayerPrefs.GetString("PersonalWord") == "") //if is the first time ...
        {
            ListPersonalWords listPersonalWords = Resources.Load<ListPersonalWords>("PersonalList-SO");
            if (listPersonalWords != null)
            {
                WordsList = listPersonalWords.WordsList;
            }
        }
        else
        {
            //get json and convert to list
            serializedJson = PlayerPrefs.GetString("PersonalWord");
            WordsList = JsonConvert.DeserializeObject<List<ListContent>>(serializedJson);
            
        }
    }

    public void SaveData()
    {
        serializedJson = JsonConvert.SerializeObject(WordsList, Formatting.None);
        PlayerPrefs.SetString("PersonalWord", serializedJson);
    }
    public void AddWord(int ListIndex,string word)
    {
        WordsList[ListIndex].Words.Add(word);
        SaveData();
    }
    public void RemoveWord(int ListIndex,int index)
    {
        WordsList[ListIndex].Words.RemoveAt(index);
        SaveData();
    }
    
    public void AddGroup(string name)
    {
        listContentForNewGroup = new ListContent();
        listContentForNewGroup.ListName = name;
        WordsList.Add(listContentForNewGroup);
        SaveData();
    }

    public void RemoveGroup(int index)
    {
        WordsList.RemoveAt(index);
        SaveData();
    }

    public void GetAllPersonalTable(ref List<string> Items)
    {
        foreach (var item in WordsList)
        {
            if(item.Words.Count != 0)
            Items.Add(item.ListName);
        }
    }

}
