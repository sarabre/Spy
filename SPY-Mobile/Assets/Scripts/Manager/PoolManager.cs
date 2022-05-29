using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UPersian.Components;
using System;

public class PoolManager : MonoBehaviour
{
    #region Personal List
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


    public string NamePersonalWordList(int index)
    {
        return PersonalWords[index].ListName;
    }

  
    public void UpdatePersonalList()
    {
        //disable all
        DisabledListBtn();
        //enable currect
        EnabledListBtn();
    }

    public void UpdatePersonalWords(int index)
    {
        //disable all
        DisabledAllPersonalWordsBtn();
        //enable currect
        EnabledPersonalWordsBtn(index);
    }

    #endregion

    #region Public List

   
    private int BtnCount
    {
        get
        {
            return SingeltonManager.Instance.publicWordsManager.BtnCount;
        }
    }
    private List<GameObject> PublicListBtn
    {
        get
        {
            return SingeltonManager.Instance.publicListPool.pooledObjects;
        }
    }

    private List<WordGroup> PublicWordBtn
    {
        get
        {
            return SingeltonManager.Instance.publicWordPool.pooledObjects;
        }
    }

    private List<Table> PublicWords
    {
        get
        {
            return SingeltonManager.Instance.publicWordsManager.Tables;
        }
    }
    private List<TableDetail> PublicTables
    {
        get
        {
            return SingeltonManager.Instance.publicWordsManager.TablesName;
        }
    }

    public void EnabledPublicListBtn()
    {
        for (int i = 0; i < BtnCount; i++)
        {
            if (PublicTables[i].ID % 50000 == i+1) // mean this is full or count of words id more than 0
            {
           
                PublicListBtn[i].GetComponentInChildren<RtlText>().text = PublicTables[i].Name ;
                PublicListBtn[i].SetActive(true);
            }
        }
    }

    public void EnabledPublicListWord(int index)
    {
        for (int i = 0; i < PublicWords[index].Word.Count; i++)
        {
            PublicWordBtn[index].wordGroup[i].GetComponent<RtlText>().text = PublicWords[index].Word[i].Word + " : " + (i + 1).ToString();
            PublicWordBtn[index].wordGroup[i].SetActive(true);
        }
    }

    public void DisabledPublicListWord()
    {
        for (int i = 0; i < PublicWords.Count; i++)
        {
            for (int j = 0; j < PublicWords[i].Word.Count; j++)
            {
                PublicWordBtn[i].wordGroup[j].SetActive(false);
            }
        }
    }

    public void UpdatePublicList()
    {
        EnabledPublicListBtn();
    }
    public void UpdatePublicWords(int index)
    {
        DisabledPublicListWord();
        EnabledPublicListWord(index);
    }


    #endregion
}
