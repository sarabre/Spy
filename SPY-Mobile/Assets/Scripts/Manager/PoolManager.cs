using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
            Debug.Log(PersonalListWords[i].ListName);
            ListPersonalListBtn[i].GetComponentInChildren<UPersian.Components.RtlText>().text = PersonalListWords[i].ListName;
            ListPersonalListBtn[i].SetActive(true);
        }

       
    }

    private void Start()
    {
        
        EnabledPersonalListBtn();
    }
}
