using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonalListPool : MonoBehaviour
{
     
     public List<GameObject> pooledObjects;
     [SerializeField] GameObject objectToPool;
     [SerializeField] Transform FatherObjectTransform;
     private List<ListContent> PersonalListWords
     {
         get
         {
             return SingeltonManager.Instance.personalWordsManager.WordsList;
         }
     }
     private int amountToPool ;

     void Awake()
     {
        
         amountToPool = PersonalListWords.Count;
    }
     void Start()
     {
         pooledObjects = new List<GameObject>();
         GameObject tmp;
         for (int i = 0; i < amountToPool; i++)
         {
             tmp = Instantiate(objectToPool);
             tmp.transform.parent = FatherObjectTransform;
             tmp.SetActive(false);
             pooledObjects.Add(tmp);
         }
     }

     public GameObject GetPooledObject()
     {
         for (int i = 0; i < amountToPool; i++)

         {
             if (!pooledObjects[i].activeInHierarchy)
             {
                 return pooledObjects[i];
             }
         }
         return null;
     }
    
}