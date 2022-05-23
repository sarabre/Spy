using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicListPool : MonoBehaviour
{
    public List<GameObject> pooledObjects = new List<GameObject>();
    [SerializeField] GameObject objectToPool;
    [SerializeField] Transform FatherObjectTransform;

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
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.transform.parent = FatherObjectTransform;
            QuantifyObject(i, tmp);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    void QuantifyObject(int index, GameObject tmp)
    {
        tmp.GetComponent<IDGenerator>().ListID = index;
        tmp.GetComponent<IDGenerator>().IsPublic = true;
    }
}
