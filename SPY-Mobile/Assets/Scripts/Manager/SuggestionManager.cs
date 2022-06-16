using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuggestionManager : MonoBehaviour
{
    public List<SuggestedWord> SuggestedWordTable = new List<SuggestedWord>();
    public SuggestedWord suggestedWord = new SuggestedWord();

    public int WordCount;

    private List<SuggestedWord> SuggestionFromDataBase
    {
        get
        {
            return SingeltonManager.Instance.wordGroupControler.SuggestedWordTable;
        }

    }

    public void GetSuggestion()
    {
        StartCoroutine(GetSuggestionWord());
    }

    IEnumerator GetSuggestionWord()
    {
        //Get Suggestion
        SingeltonManager.Instance.wordGroupControler.GetSuggestedWord();

        yield return new WaitForSeconds(1f);

        //Make Private
        SortList();
        //Call Pool
        SingeltonManager.Instance.suggestedWordPool.CreatePool();
    }

    public void SortList()
    {
        for (int i = 0; i < SuggestionFromDataBase.Count ; i++)
        {
            suggestedWord = SuggestionFromDataBase[i];
            SuggestedWordTable.Add(suggestedWord);
            suggestedWord = new SuggestedWord();
        }
        WordCount = SuggestionFromDataBase.Count;
    }

    public void SendSuggestion(string NewWord,string WordGroupCode)
    {
        SingeltonManager.Instance.wordGroupControler.SendSuggestedWord(NewWord, WordGroupCode);
    }
}
