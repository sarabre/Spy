using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PersonalWordPool : MonoBehaviour
{
   
   
    public List<WordGroup> pooledObjects  = new List<WordGroup>();
    //public WordGroup wordGroupclass = new WordGroup();

    List<GameObject> lG;
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
       // p[1][0].
      
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
    void Start()
    { 
        GameObject tmp;

        for (int j = 0; j < PersonalListWords.Count; j++)
        {

            WordGroup wordGroupclass = new WordGroup();
        
            pooledObjects.Add(wordGroupclass);

            for (int i = 0; i < PersonalListWords[j].Words.Count; i++)
            {

                tmp = Instantiate(objectToPool);
                tmp.transform.parent = FatherObjectTransform;
                tmp.SetActive(false);
                pooledObjects[j].wordGroup.Add(tmp);


            }

            //wordGroupclass.wordGroup.Clear();
        } 
    }
}

[Serializable] 
public class WordGroup
{
    [SerializeField] public List<GameObject> wordGroup = new List<GameObject>() ;
}