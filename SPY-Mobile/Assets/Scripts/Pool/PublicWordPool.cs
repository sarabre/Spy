using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicWordPool : MonoBehaviour
{


    public List<WordGroup> pooledObjects = new List<WordGroup>();



    [SerializeField] GameObject objectToPool;
    [SerializeField] Transform FatherObjectTransform;



    private int amountToPoolChild
    {
        get
        {
            return SingeltonManager.Instance.publicWordsManager.WordCount;
        }
    }

    private int amountToPool
    {
        get
        {
            return SingeltonManager.Instance.publicWordsManager.BtnCount;
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
                tmp.transform.SetParent(FatherObjectTransform);
                QuantifyObject(i, j, tmp);
                tmp.SetActive(false);
                pooledObjects[j].wordGroup.Add(tmp);


            }


        }

    }



    private void QuantifyObject(int index, int fatherindex, GameObject tmp)
    {
        tmp.GetComponent<IDGenerator>().ID = index;
        tmp.GetComponent<IDGenerator>().FatherID = fatherindex;
        tmp.GetComponent<IDGenerator>().IsPublic = true;
    }
}
