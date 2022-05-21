using System.Collections;
using System.Collections.Generic;
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


    // should Quantify in RUN time
    public void Awake()
    {
        ListPersonalWords listPersonalWords = Resources.Load<ListPersonalWords>("PersonalList-SO");
        if (listPersonalWords != null)
        {
            WordsList = listPersonalWords.WordsList;
        }
    }

    public void AddWord(int ListIndex,string word)
    {
        WordsList[ListIndex].Words.Add(word);
    }
    public void RemoveWord(int ListIndex,int index)
    {
        WordsList[ListIndex].Words.RemoveAt(index);
    }
    
    public void AddGroup(string name)
    {
        listContentForNewGroup.ListName = name;
        WordsList.Add(listContentForNewGroup);
    }

    public void RemoveGroup(int index)
    {
        WordsList.RemoveAt(index);
    }

}
