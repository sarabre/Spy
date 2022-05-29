using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;


public class WordGroupController : MonoBehaviour
{
    //private string secretKey = "2003";
    //public string addScoreURL =
           // "http://localhost/HighScoreGame/addscore.php?";

    public string GetTableURL = "http://localhost/Spy/GetTables.php?";
    public string GetTableWordsURL = "http://localhost/Spy/GetTableWords.php?";




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

    public void Awake()
    {
        StartCoroutine(GetData());
    }

    #region Get-Data


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

                        TablesNameFromDataBase[CountLoopGetTable].NameCode = splitData[i-1];
                        CountForCheckingIf = 0;
                        CountLoopGetTable++;
                        tableDetail = new TableDetail();
                    }
                    else if (CountForCheckingIf == 1)
                    {

                        TablesNameFromDataBase[CountLoopGetTable].Name = splitData[i-1];
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


    public void SendScoreBtn()
    {
        //  StartCoroutine(PostScores(nameTextInput.text,
        //    Convert.ToInt32(scoreTextInput.text)));
        //nameTextInput.gameObject.transform.parent.GetComponent<InputField>().text = "";
        //scoreTextInput.gameObject.transform.parent.GetComponent<InputField>().text = "";
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
}
