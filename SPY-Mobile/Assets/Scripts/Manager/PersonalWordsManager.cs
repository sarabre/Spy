using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonalWordsManager : MonoBehaviour
{
    public List<ListContent> WordsList = new List<ListContent>();
   
    public void Awake()
    {
        ListPersonalWords listPersonalWords = Resources.Load<ListPersonalWords>("PersonalList-SO");
        if (listPersonalWords != null)
        {
            WordsList = listPersonalWords.WordsList;
        }
    }

}
