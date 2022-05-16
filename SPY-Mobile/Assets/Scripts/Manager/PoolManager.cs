using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UPersian.Components;
using System;

public class PoolManager : MonoBehaviour
{
    List<GameObject> ListPersonalListBtn
    {
        get
        {
            return SingeltonManager.Instance.personalListPool.pooledObjects;
        }
    }

    List<WordGroup> ListPersonalWorsdBtn
    {
        get
        {
            return SingeltonManager.Instance.personalWordPool.pooledObjects;
        }
    }

    private List<ListContent> PersonalListWords
    {
        get
        {
            return SingeltonManager.Instance.personalWordsManager.WordsList;
        }
    }


    void EnabledPersonalListBtn()
    {
       
        for (int i = 0; i < ListPersonalListBtn.Count ; i++)
        {
            ListPersonalListBtn[i].GetComponentInChildren<RtlText>().text = PersonalListWords[i].ListName;
            ListPersonalListBtn[i].SetActive(true);
        }

       
    }
    public void EnabledPersonalWordsBtn(int index)
    {
       
        for (int i = 0; i < ListPersonalWorsdBtn[index].wordGroup.Count ; i++)
        {
            ListPersonalWorsdBtn[index].wordGroup[i].GetComponentInChildren<RtlText>().text = PersonalListWords[index].Words[i] +" : " + (i + 1).ToString() ;
            ListPersonalWorsdBtn[index].wordGroup[i].SetActive(true);

        }
    }
     public void DisabledAllPersonalWordsBtn()
    {
        for (int i = 0; i < ListPersonalWorsdBtn.Count; i++)
        {
            for (int j = 0; j < ListPersonalWorsdBtn[i].wordGroup.Count ; j++)
            {
                ListPersonalWorsdBtn[i].wordGroup[j].SetActive(false);
            }
        }
    }

    public string NameWordList(int index)
    {
        return PersonalListWords[index].ListName;
    }

    private void Start()
    {  
        EnabledPersonalListBtn();
    }

    public void AddObject(GameObject Object,int listIndex,int index)
    {
        Object.GetComponent<RtlText>().text = PersonalListWords[listIndex].Words[index-1] + " : " + (index).ToString();
        Object.SetActive(true);
    }

    public void UpdateListIndexAfterRemove(int startPoint,int ListIndex)
    {
        for (int i = startPoint; i < ListPersonalWorsdBtn[ListIndex].wordGroup.Count ; i++)
        {
            ListPersonalWorsdBtn[ListIndex].wordGroup[i].GetComponent<RtlText>().text = PersonalListWords[ListIndex].Words[i] + " : " + (i+1).ToString(); ;
        }
    }
    

}
