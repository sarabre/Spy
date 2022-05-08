using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = "PersonalList-SO", menuName = "Personal")]
public class ListPersonalWords : ScriptableObject
{
    public List<ListContent> WordsList = new List<ListContent>();

}

[Serializable]
[SerializeField]
public class ListContent
{
    public string ListName;
    public List<string> Words = new List<string>();
}
