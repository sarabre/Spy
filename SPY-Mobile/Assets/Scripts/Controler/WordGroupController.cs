using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;


public class WordGroupController : MonoBehaviour
{
    public void Awake()
    {
        StartCoroutine(GetData());
        //SendSuggestedWord("suggestedWord2", "wg-02");
       
        
        

        //RemoveWord(5000102, "wg-01");
    }

    

    #region Get-Data

    string GetTableURL = "http://localhost/Spy/GetTables.php?";
    string GetTableWordsURL = "http://localhost/Spy/GetTableWords.php?";

    public List<TableDetail> TablesNameFromDataBase = new List<TableDetail>();
    public TableDetail tableDetail = new TableDetail();

    public List<Table> Tables = new List<Table>();
    public Table table = new Table();
    public WordDetail wordDetail = new WordDetail();



    int CountLoopGetTable;
    int CountLoopGetTableWord;
    int WordCount;
    int CountForCheckingIf = 0;

    string code;

    IEnumerator GetData()
    {
        yield return StartCoroutine(GetTable());
        StartCoroutine(GetWords());
    }

    IEnumerator GetTable()
    {
        UnityWebRequest hs_get = UnityWebRequest.Get(GetTableURL);
        yield return hs_get.SendWebRequest();

        if (hs_get.error != null)
            Debug.Log("There was an error getting the high score: "
                    + hs_get.error);
        else
        {
            string dataText = hs_get.downloadHandler.text;

            MatchCollection mc = Regex.Matches(dataText, @"_");

            if (mc.Count > 0)
            {
                string[] splitData = Regex.Split(dataText, @"_");
                CountLoopGetTable = 0;
                for (int i = 1; i <= mc.Count; i++)
                {

                    if (CountForCheckingIf == 2)
                    {

                        TablesNameFromDataBase[CountLoopGetTable].NameCode = splitData[i - 1];
                        CountForCheckingIf = 0;
                        CountLoopGetTable++;
                        tableDetail = new TableDetail();
                    }
                    else if (CountForCheckingIf == 1)
                    {

                        TablesNameFromDataBase[CountLoopGetTable].Name = splitData[i - 1];
                        CountForCheckingIf = 2;
                    }
                    else
                    {

                        TablesNameFromDataBase.Add(tableDetail);
                        TablesNameFromDataBase[CountLoopGetTable].ID = int.Parse(splitData[i - 1]);
                        CountForCheckingIf = 1;
                    }

                }
            }
        }

        SingeltonManager.Instance.publicWordsManager.SortTable();
    }

    IEnumerator GetWords()
    {

        for (int j = 0; j < TablesNameFromDataBase.Count; j++)
        {
            code = TablesNameFromDataBase[j].NameCode;
            wordDetail = new WordDetail();

            UnityWebRequest hs_get = UnityWebRequest.Get(GetTableWordsURL + "Code=" + code + "");
            yield return hs_get.SendWebRequest();

            if (hs_get.error != null)
                Debug.Log("There was an error getting the high score: " + hs_get.error);
            else
            {
                string dataText = hs_get.downloadHandler.text;
                MatchCollection mc = Regex.Matches(dataText, @"_");
                if (mc.Count > 0)
                {
                    Tables.Add(table);

                    string[] splitData = Regex.Split(dataText, @"_");
                    for (int i = 0; i < mc.Count; i++)
                    {
                        if (i % 2 == 0)
                        {
                            Tables[CountLoopGetTableWord].TableNameCode = code;

                            Tables[CountLoopGetTableWord].Word.Add(wordDetail);
                            Tables[CountLoopGetTableWord].Word[WordCount].ID = int.Parse(splitData[i]);


                        }
                        else
                        {
                            Tables[CountLoopGetTableWord].Word[WordCount].Word = splitData[i];
                            wordDetail = new WordDetail();
                            WordCount++;
                        }
                    }

                    WordCount = 0;
                    CountLoopGetTableWord++;
                    table = new Table();

                }
            }

        }
        SingeltonManager.Instance.publicWordsManager.SortWords();


    }

    #endregion

    #region Send Suggested words

    string AddToSuggested = "http://localhost/Spy/AddToSuggested.php?";

    public void SendSuggestedWord(string word, string wgName)
    {
        int wgCode = SingeltonManager.Instance.publicWordsManager.GetCodeOfTable(wgName);
        StartCoroutine(PostSuggestedWord(word, wgName, wgCode));
    }
    IEnumerator PostSuggestedWord(string word, string wgName, int wgCode)
    {
        string hash = HashInput(word);
        string post_url = AddToSuggested + "word=" + word + "&wgcode=" + wgCode + "&wgname=" + wgName + "&hash=" + hash;
        UnityWebRequest hs_post = UnityWebRequest.Post(post_url, hash);
        yield return hs_post.SendWebRequest();
        if (hs_post.error != null)
            Debug.Log("There was an error posting the high score: " + hs_post.error);
    }

    public string HashInput(string input)
    {
        SHA256Managed hm = new SHA256Managed();
        byte[] hashValue =
                hm.ComputeHash(System.Text.Encoding.ASCII.GetBytes(input));
        string hash_convert =
                 BitConverter.ToString(hashValue).Replace("-", "").ToLower();
        return hash_convert;
    }

    #endregion

    #region Check Admin

    
    string CheckAdminURL = "http://localhost/Spy/CheckAdmin.php?";
    public void CheckAdmin(string username, string password)
    {

        StartCoroutine(CheckAdmins(username, password));

    }


  IEnumerator CheckAdmins(string username,string password)
    {
        UnityWebRequest hs_get = UnityWebRequest.Get(CheckAdminURL + "UserName=" + username + "&Password="+password);
        yield return hs_get.SendWebRequest();

        if (hs_get.error != null)
            Debug.Log("There was an error getting the high score: "
                    + hs_get.error);
        else
        {
            string dataText = hs_get.downloadHandler.text;
            MatchCollection mc = Regex.Matches(dataText, @"_");

            if (mc.Count > 0)
            {
                string[] splitData = Regex.Split(dataText, @"_");
                SingeltonManager.Instance.profile.name = splitData[0];
                SingeltonManager.Instance.profile.UserSit = UserSituation.Admin;
            }
            else
            {
                SingeltonManager.Instance.profile.UserSit = UserSituation.Player;
            }
        }

        
    }
    
    #endregion

    #region Get Suggested Word

    string GetSuggestedWordURL = "http://localhost/Spy/GetSuggestedWord.php?";

    public List<SuggestedWord> SuggestedWordTable = new List<SuggestedWord>();
    public SuggestedWord suggestedWord = new SuggestedWord();

    int CountLoopGetSuggestedWord;
    int CountForCheckingIfSW;
    public void GetSuggestedWord()
    {
        StartCoroutine(GetSuggestedWords());
    }
    IEnumerator GetSuggestedWords()
    {

        UnityWebRequest hs_get = UnityWebRequest.Get(GetSuggestedWordURL);
        yield return hs_get.SendWebRequest();

        if (hs_get.error != null)
            Debug.Log("There was an error getting the high score: "
                    + hs_get.error);
        else
        {
            string dataText = hs_get.downloadHandler.text;

            MatchCollection mc = Regex.Matches(dataText, @"_");
            if (mc.Count > 0)
            {
                string[] splitData = Regex.Split(dataText, @"_");
                CountLoopGetSuggestedWord = 0;
                for (int i = 1; i <= mc.Count; i++)
                {

                    if (CountForCheckingIfSW == 2)
                    {

                        SuggestedWordTable[CountLoopGetSuggestedWord].WgName = splitData[i - 1];
                        CountForCheckingIfSW = 0;
                        CountLoopGetSuggestedWord++;
                        suggestedWord = new SuggestedWord();
                    }
                    else if (CountForCheckingIfSW == 1)
                    {

                        SuggestedWordTable[CountLoopGetSuggestedWord].WgCode = int.Parse(splitData[i - 1]);
                        CountForCheckingIfSW = 2;
                    }
                    else
                    {

                        SuggestedWordTable.Add(suggestedWord);
                        SuggestedWordTable[CountLoopGetSuggestedWord].Word = splitData[i - 1];
                        CountForCheckingIfSW = 1;
                    }

                }
            }
        }
    }


    #endregion

    #region Accept Suggested Word

    string AddSuggestedWordURL = "http://localhost/Spy/AddToTables.php?";

    int WgID;
    int WordID;

    public void AddWord(string word,string WgCode)
    {
        WgID = SingeltonManager.Instance.publicWordsManager.GetCodeOfTable(WgCode);
        WordID = SingeltonManager.Instance.publicWordsManager.GetNewWordIndex(WgCode);

        if (WordID != 0)
            StartCoroutine(AddSuggestedWord(word, WordID, WgCode, WgID));
        else
            Debug.Log("Not Valid Word Group Name");

    }
    IEnumerator AddSuggestedWord(string word, int wordID, string WgCode, int WgID)
    {
       
        string hash = HashInput(word);
        string post_url = AddSuggestedWordURL + "TableCode=" + WgCode.Replace(" ", String.Empty) + "&WordID=" + wordID + "&Word=" + word.Replace(" ", String.Empty) + "&TableID=" + WgID;
        UnityWebRequest hs_post = UnityWebRequest.Post(post_url, hash);
        yield return hs_post.SendWebRequest();
        Debug.Log(post_url);
        if (hs_post.error != null)
            Debug.Log("There was an error posting the high score: " + hs_post.error);
    }



    #endregion

    #region Reject Suggested Word 

    string RemoveSuggestedWordURL = "http://localhost/Spy/RemoveFromSuggestion.php?";

   

    public void RemoveSuggestion(string word)
    {
        
        StartCoroutine(RemoveSuggestedWord(word));

    }
    IEnumerator RemoveSuggestedWord(string word)
    {
       
        string hash = HashInput(word);
        string post_url = RemoveSuggestedWordURL + "Word=" + word.Replace(" ", String.Empty);
        Debug.Log(post_url);
        UnityWebRequest hs_post = UnityWebRequest.Post(post_url, hash);
        yield return hs_post.SendWebRequest();
        if (hs_post.error != null)
            Debug.Log("There was an error posting the high score: " + hs_post.error);
    }

    #endregion

    #region Remove Word From Table

    string RemoveWordURL = "http://localhost/Spy/RemoveFromWordGroup.php?";

    
    public void RemoveWord(int wordID, string WgCode)
    {
        WgID = SingeltonManager.Instance.publicWordsManager.GetCodeOfTable(WgCode);

        StartCoroutine(RemoveWord(wordID, WgCode, WgID));
       
    }
    IEnumerator RemoveWord(int wordID, string WgCode, int WgID)
    {

        string hash = HashInput(WgCode);
        string post_url = RemoveWordURL + "TableCode=" + WgCode + "&WordID=" + wordID + "&TableID=" + WgID;
        UnityWebRequest hs_post = UnityWebRequest.Post(post_url, hash);
        yield return hs_post.SendWebRequest();
        if (hs_post.error != null)
            Debug.Log("There was an error posting the high score: " + hs_post.error);
    }

    #endregion
}
