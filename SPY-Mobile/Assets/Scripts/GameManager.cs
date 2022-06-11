using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<string> Words = new List<string>();
   
    public List<AllWords> allWords = new List<AllWords>();
    AllWords allwords = new AllWords();

    public List<int> CurrentGameSelectedWordIndex = new List<int>();

    public List<int> CurrentRoundSpyIndex = new List<int>();

    public int PlayersKnowThierRoleCount = 0;
    bool IsSpy;

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

        Words.Clear();

        foreach (var item in allWords[index].Words)
        {
            this.Words.Add(item);
        }


       
    }

    public void WhoIsSpy()
    {
        if (CurrentRoundSpyIndex.Count != SingeltonManager.Instance.team.NumberOfSpy)
        {
            int x = UnityEngine.Random.Range(0, SingeltonManager.Instance.team.NumberOfPlayer);
            foreach (var item in CurrentRoundSpyIndex)
            {
                if (x == item)
                {
                    WhoIsSpy();
                   
                }
                
            }

            CurrentRoundSpyIndex.Add(x);

        }
        else
        {
            CurrentRoundSpyIndex.Sort();
            return;
        }
                    WhoIsSpy();
    }
    public void ManageGame()
    {
        //determine Spy Index
        WhoIsSpy();

        //manage GamePlay Panel
        GivePhoneToPlayer(0);

        //StartCoroutine(ManagingTime());
    }
    IEnumerator ManagingTime()
    {
        yield return new WaitForSeconds(2f);
    }

    public void GivePhoneToPlayer(int index)
    {
        SingeltonManager.Instance.canvasManager.DeterminePlayerName(SingeltonManager.Instance.team.players[index].name);
        PlayersKnowThierRoleCount++;
    }

    public void NextStepInGamePlay() //Next Btn
    {

    }

    public void ImInGamePlay() // I am Btn
    {
      
        foreach (var item in CurrentRoundSpyIndex)
        {
            if(PlayersKnowThierRoleCount == item)
            {
                IsSpy = true;
                return;
            }
            else
            {
                IsSpy = false;
            }
        }
        
        SingeltonManager.Instance.canvasManager.ShowPlayerRole(IsSpy, ThisRoundWord());
    }

    public string ThisRoundWord()
    {
        int WordIndex = UnityEngine.Random.Range(0, Words.Count);
        foreach (var item in CurrentGameSelectedWordIndex)
        {
            //if(CurrentGameSelectedWordIndex == Words.Count) //should show word
           
        }
        CurrentGameSelectedWordIndex.Add(WordIndex);
        return "";
    }
}

[Serializable]
public class AllWords
{
    public List<string> Words = new List<string>();
}
