using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UPersian.Components;

public class SuggestedWordPool : MonoBehaviour
{
    public List<GameObject> pooledObjects = new List<GameObject>();
    [SerializeField] GameObject objectToPool;
    [SerializeField] Transform FatherObjectTransform;

    public string AddedText; //For Persian Write
    public string ToList;

    

    private int amountToPool
    {
        get
        {
            return SingeltonManager.Instance.suggestionManager.WordCount;
        }
    }

    public void CreatePool()
    {

        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.transform.SetParent(FatherObjectTransform);
            QuantifyObject(i, tmp);
            tmp.SetActive(true);
            pooledObjects.Add(tmp);
        }
    }

    public void QuantifyObject(int index,GameObject tmp)
    {
        string word = SingeltonManager.Instance.suggestionManager.SuggestedWordTable[index].Word;
        string WGCode = SingeltonManager.Instance.suggestionManager.SuggestedWordTable[index].WgName;
        string WGName = SingeltonManager.Instance.publicWordsManager.GetNameOfTableByCode(SingeltonManager.Instance.suggestionManager.SuggestedWordTable[index].WgName);
        tmp.GetComponent<IDGenerator>().word = word;
        tmp.GetComponent<IDGenerator>().WordGroupCode = WGCode;
        tmp.GetComponentInChildren<RtlText>().text = $"< {word} > {ToList} < {WGName} > {AddedText} ";
    }

}
