using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonalListPool : MonoBehaviour
{
     
     public List<GameObject> pooledObjects = new List<GameObject>();
     [SerializeField] GameObject objectToPool;
     [SerializeField] Transform FatherObjectTransform;
     private List<ListContent> PersonalListWords
     {
         get
         {
             return SingeltonManager.Instance.personalWordsManager.WordsList;
         }
     }
     private int amountToPool
    {
        get
        {
            return SingeltonManager.Instance.personalWordsManager.WordsList.Count;
        }
    }

     void Start()
     {
        
         GameObject tmp;
         for (int i = 0; i < amountToPool; i++)
         {
             tmp = Instantiate(objectToPool);
             tmp.transform.parent = FatherObjectTransform;
             QuantifyObject(i, tmp);
             tmp.SetActive(false);
             pooledObjects.Add(tmp);
         }
     }

    public void RemoveBtn(int index)
    {
        Destroy(pooledObjects[index]);
        pooledObjects.RemoveAt(index);
    }
    
    public void AddBtn(int index)
    {
      
        GameObject tmp = Instantiate(objectToPool, FatherObjectTransform);
        QuantifyObject(index, tmp);
        tmp.SetActive(false);
        pooledObjects.Add(tmp);
        SingeltonManager.Instance.poolManager.AddBtn(tmp, index);
    }

    void QuantifyObject(int index,GameObject tmp)
    {
        tmp.GetComponent<IDGenerator>().ListID = index;
    }
}
