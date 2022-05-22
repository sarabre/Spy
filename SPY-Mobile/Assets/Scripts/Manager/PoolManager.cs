using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UPersian.Components;
using System;

public class PoolManager : MonoBehaviour
{
    private List<GameObject> PersonalListBtn
    {
        get
        {
            return SingeltonManager.Instance.personalListPool.pooledObjects;
        }
    }

    private List<WordGroup> PersonalWorsdBtn
    {
        get
        {
            return SingeltonManager.Instance.personalWordPool.pooledObjects;
        }
    }

    private List<ListContent> PersonalWords
    {
        get
        {
            return SingeltonManager.Instance.personalWordsManager.wordlist;
        }
    }


    public void EnabledListBtn()
    {
       
        for (int i = 0; i < PersonalWords.Count ; i++)
        {
            PersonalListBtn[i].GetComponentInChildren<RtlText>().text = PersonalWords[i].ListName + " : " + (i + 1).ToString();
            PersonalListBtn[i].SetActive(true);
        }

       
    }

    public void EnabledPersonalWordsBtn(int index)
    {
       
        for (int i = 0; i < PersonalWords[index].Words.Count ; i++)
        {
            PersonalWorsdBtn[index].wordGroup[i].GetComponentInChildren<RtlText>().text = PersonalWords[index].Words[i] +" : " + (i + 1).ToString() ;
            PersonalWorsdBtn[index].wordGroup[i].SetActive(true);

        }
    }

    public void DisabledAllPersonalWordsBtn()
    {
        Debug.Log(PersonalWorsdBtn.Count);
        for (int i = 0; i < PersonalWorsdBtn.Count; i++)
        {
            for (int j = 0; j < PersonalWorsdBtn[i].wordGroup.Count ; j++)
            {
                PersonalWorsdBtn[i].wordGroup[j].SetActive(false);
            }
        }
    }

    public void DisabledListBtn()
    {
        for (int i = 0; i < PersonalListBtn.Count; i++)
        {
            PersonalListBtn[i].SetActive(false);
        }
    }


    public string NameWordList(int index)
    {
        return PersonalWords[index].ListName;
    }

  
    public void UpdateList()
    {
        //disable all
        DisabledListBtn();
        //enable currect
        EnabledListBtn();
    }

    public void UpdateWords(int index)
    {
        //disable all
        DisabledAllPersonalWordsBtn();
        //enable currect
        EnabledPersonalWordsBtn(index);
    }

   

}
