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
            return SingeltonManager.Instance.personalWordsManager.WordsList;
        }
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
                QuantifyObject(i+1, j, tmp);
                tmp.SetActive(false);
                pooledObjects[j].wordGroup.Add(tmp);


            }

            
        }

        DetermineFieldParent();

    }
    void DetermineFieldParent()
    {
        Field.transform.parent = FatherObjectTransform.parent;
        Field.transform.parent = FatherObjectTransform;
    }

    public void RemoveObject(int index, int listIndex)
    {
        Destroy(pooledObjects[listIndex].wordGroup[index]);
        pooledObjects[listIndex].wordGroup.RemoveAt(index);
    }
    public void AddObject(int index, int listIndex)
    {
        GameObject tmp = Instantiate(objectToPool,FatherObjectTransform);
        QuantifyObject(index, listIndex, tmp);
        pooledObjects[listIndex].wordGroup.Add(tmp);
        tmp.SetActive(false);
        SingeltonManager.Instance.poolManager.AddObject(tmp, listIndex, index);
        DetermineFieldParent();
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