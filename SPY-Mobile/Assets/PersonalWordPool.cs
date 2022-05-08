using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PersonalWordPool : MonoBehaviour
{
   
    [SerializeField] public List<WordGroup> pooledObjects;
    [SerializeField] WordGroup wordGroupclass;
    [SerializeField] GameObject objectToPool;
    [SerializeField] Transform FatherObjectTransform;
    private int PersonalListWordsCount;
    private List<ListContent> PersonalListWords
    {
        get
        {
            return SingeltonManager.Instance.personalWordsManager.WordsList;
        }
    }
    private int amountToPool;

    void Awake()
    {

      
    }
    void Start()
    {
        pooledObjects = new List<WordGroup>();
        GameObject tmp;
        Debug.Log(PersonalListWords.Count + "   PersonalListWords.Count  ");
        Debug.Log(PersonalListWords[0].Words.Count + "   PersonalListWords[0].Words.Count  ");

        for (int j= 0; j < PersonalListWords.Count ; j++)
        {
            
            for (int i = 0; i < PersonalListWords[j].Words.Count ; i++)
            {
                tmp = Instantiate(objectToPool);
                tmp.transform.parent = FatherObjectTransform;
                tmp.SetActive(false);
                Debug.Log("add");
                wordGroupclass.wordGroup.Add(tmp);
            }
            Debug.Log("clear");
            pooledObjects.Add(wordGroupclass);
            
        }
        
    }

    public GameObject GetPooledObject()
    {
        for (int j = 0; j < PersonalListWords.Count; j++)
        {
            for (int i = 0; i < PersonalListWords[j].Words.Count; i++)

            {
                if (!pooledObjects[j].wordGroup[i].activeInHierarchy)
                {
                    return pooledObjects[j].wordGroup[i];
                }
            }
        }
        return null;
    }
}


[Serializable] 
public class WordGroup
{
    [SerializeField] public List<GameObject> wordGroup ;
}