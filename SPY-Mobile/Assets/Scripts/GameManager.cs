using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<string> Words = new List<string>();

    public List<AllWords> allWords = new List<AllWords>();
    AllWords allwords = new AllWords();

   
    public void MakeListOfWord(int index)
    {
        for (int i = 0; i < 3; i++) // All //AllPublic //AllPersonal
        {
            allWords.Add(allwords);
            allwords = new AllWords();
        }
       
        for (int i = 0; i < SingeltonManager.Instance.wordGroupControler.Tables.Count; i++)
        {
            if (SingeltonManager.Instance.wordGroupControler.Tables[i].Word.Count != 0)
            {
                allWords.Add(allwords);
                allwords = new AllWords();
            }

            for (int j = 0; j < SingeltonManager.Instance.wordGroupControler.Tables[i].Word.Count; j++)
            {
                if(SingeltonManager.Instance.wordGroupControler.Tables[i].Word.Count != 0)
                {
                    allWords[0].Words.Add(SingeltonManager.Instance.wordGroupControler.Tables[i].Word[j].Word);
                    allWords[1].Words.Add(SingeltonManager.Instance.wordGroupControler.Tables[i].Word[j].Word);
                    allWords[i+3].Words.Add(SingeltonManager.Instance.wordGroupControler.Tables[i].Word[j].Word);
                }
            }
        }

        for (int i = 0; i < SingeltonManager.Instance.personalWordsManager.wordlist.Count; i++)
        {
            if (SingeltonManager.Instance.personalWordsManager.wordlist[i].Words.Count != 0)
            {
                allWords.Add(allwords);
                allwords = new AllWords();
            }

            for (int j = 0; j < SingeltonManager.Instance.personalWordsManager.wordlist[i].Words.Count; j++)
            {
                if (SingeltonManager.Instance.personalWordsManager.wordlist[i].Words.Count != 0)
                {
                    allWords[0].Words.Add(SingeltonManager.Instance.personalWordsManager.wordlist[i].Words[j].ToString());
                    allWords[2].Words.Add(SingeltonManager.Instance.personalWordsManager.wordlist[i].Words[j].ToString());
                    allWords[i + SingeltonManager.Instance.wordGroupControler.Tables.Count + 2].Words.Add(SingeltonManager.Instance.personalWordsManager.wordlist[i].Words[j].ToString());
                }
            }
        }

        foreach (var item in allWords[index].Words)
        {
            this.Words.Add(item);
        }
    }


}

[Serializable]
public class AllWords
{
    public List<string> Words = new List<string>();
}
