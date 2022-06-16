using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PersonalWordPool : MonoBehaviour
{
   
   
    public List<WordGroup> pooledObjects  = new List<WordGroup>();


    [SerializeField] GameObject Field;
    [SerializeField] GameObject objectToPool;
    [SerializeField] Transform FatherObjectTransform;
    
    private int PersonalListWordsCount;
    private List<ListContent> PersonalListWords
    {
        get
        {
            return SingeltonManager.Instance.personalWordsManager.wordlist;
        }
    }

    private int amountToPoolChild
    {
        get
        {
            return SingeltonManager.Instance.personalWordsManager.WordCount;
        }
    }

    private int amountToPool
    {
        get
        {
            return SingeltonManager.Instance.personalWordsManager.BtnCount;
        }
    }

    void Start()
    { 
        GameObject tmp;

        for (int j = 0; j < amountToPool; j++)
        {

            WordGroup wordGroupclass = new WordGroup();
        
            pooledObjects.Add(wordGroupclass);

            for (int i = 0; i < amountToPoolChild; i++)
            {

                tmp = Instantiate(objectToPool);
                tmp.transform.SetParent(FatherObjectTransform) ;
                QuantifyObject(i, j, tmp);
                tmp.SetActive(false);
                pooledObjects[j].wordGroup.Add(tmp);


            }

            
        }

        DetermineFieldParent();

    }
    void DetermineFieldParent()
    {
        Field.transform.SetParent(FatherObjectTransform.parent);
        Field.transform.SetParent(FatherObjectTransform);
    }


    private void QuantifyObject(int index,int fatherindex,GameObject tmp)
    {
        tmp.GetComponent<IDGenerator>().ID = index ;
        tmp.GetComponent<IDGenerator>().FatherID = fatherindex;
    }
}



[Serializable] 
public class WordGroup
{
    [SerializeField] public List<GameObject> wordGroup = new List<GameObject>() ;
}