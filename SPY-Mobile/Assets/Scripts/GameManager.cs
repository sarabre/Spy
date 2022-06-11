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

    public int PlayersKnowThierRoleCount = -1;
    bool IsSpy;
    string word;
    bool IsLastPlayer;

    public int PlayedRound = 0;

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
                    return;
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
        GivePhoneToPlayer();

        //create table
        SingeltonManager.Instance.canvasManager.CreateTableRow();

        PlayedRound++;

    }

    int sec;
    int min;
    public IEnumerator ManagingTime()
    {
        float Duration = SingeltonManager.Instance.team.RoundDuration;
        for (int i = 0; i < SingeltonManager.Instance.team.RoundDuration; i++)
        {
            Duration--;
            DetermindSecond(Duration,out min,out sec);
            yield return new WaitForSeconds(1f);
            SingeltonManager.Instance.canvasManager.Timer(sec,min);
        }
     
    }

    public void DetermindSecond(float Duration,out int min,out int sec )
    {
        
        min = (int)Math.Floor(Duration / 60);
        sec = (int)(Duration - min*60);
    }

    public void GivePhoneToPlayer()
    {
        SingeltonManager.Instance.canvasManager.DeterminePlayerName(SingeltonManager.Instance.team.players[PlayersKnowThierRoleCount].name);
        PlayersKnowThierRoleCount++;
    }

   

    public void ImInGamePlay() // I am Btn
    {
        
        foreach (var item in CurrentRoundSpyIndex)
        {
            if(PlayersKnowThierRoleCount == item)
            {
                IsSpy = true;
                break;
            }
            else
            {
                IsSpy = false;
            }
        }

        if(word == null)
        word = ThisRoundWord();

        if (PlayersKnowThierRoleCount == SingeltonManager.Instance.team.players.Count)
            IsLastPlayer = true;

        SingeltonManager.Instance.canvasManager.ShowPlayerRole(IsSpy,word ,IsLastPlayer);

        IsLastPlayer = false;
    }

    public string ThisRoundWord()
    {
        int WordIndex = UnityEngine.Random.Range(0, Words.Count);
        foreach (var item in CurrentGameSelectedWordIndex)
        {
            if (WordIndex == item)
            {
                ThisRoundWord(); 
            }


        }
        CurrentGameSelectedWordIndex.Add(WordIndex);
        return Words[WordIndex];
    }
   
    public void GiveResultOfThisRound(int SpyScore) // -1 for lose and 1 for win
    {
        StopAllCoroutines();
       
        foreach (var item in CurrentRoundSpyIndex)
            {
            SingeltonManager.Instance.team.players[item].Score += SpyScore;
        }
        
    }
}

[Serializable]
public class AllWords
{
    public List<string> Words = new List<string>();
}
