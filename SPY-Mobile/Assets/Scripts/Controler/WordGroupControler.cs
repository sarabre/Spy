using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;


public class WordGroupControler : MonoBehaviour
{
    private string secretKey = "2003";
    public string addScoreURL =
            "http://localhost/HighScoreGame/addscore.php?";
    public string GetTableURL =
             "http://localhost/Spy/GetTables.php?";

    public List<TableDetail> Tables = new List<TableDetail>();
    public TableDetail tableDetail = new TableDetail();

    int CountLoopGetTable;
    int j = 0;

    public void Start()
    {
        GetTables();
    }

    public void GetTables()
    {
       // nameResultText.text = "Player: \n \n";
       // scoreResultText.text = "Score: \n \n";
        StartCoroutine(GetTable());
    }
    public void SendScoreBtn()
    {
      //  StartCoroutine(PostScores(nameTextInput.text,
       //    Convert.ToInt32(scoreTextInput.text)));
        //nameTextInput.gameObject.transform.parent.GetComponent<InputField>().text = "";
        //scoreTextInput.gameObject.transform.parent.GetComponent<InputField>().text = "";
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
            Debug.Log(dataText);
            if (mc.Count > 0)
            {
                string[] splitData = Regex.Split(dataText, @"_");
                CountLoopGetTable = 0;
                for (int i = 1; i <= mc.Count; i++)
                {
                    
                    if (j == 2)
                    {
             
                        Tables[CountLoopGetTable].NameCode = splitData[i-1];
                        j = 0;
                        CountLoopGetTable++;
                        tableDetail = new TableDetail();
                    }
                    else if (j == 1)
                    {
                        
                        Tables[CountLoopGetTable].Name = splitData[i-1].faConvert();
                        j = 2;
                    }
                    else
                    {
                        Tables.Add(tableDetail);
                        Tables[CountLoopGetTable].ID = int.Parse(splitData[i-1]);
                        j = 1;
                    }
                    
                }
            }
        }
    }

    IEnumerator PostScores(string name, int score)
    {

        string hash = HashInput(name + score + secretKey);
        string post_url = addScoreURL + "name=" +
               UnityWebRequest.EscapeURL(name) + "&score="
               + score + "&hash=" + hash;

        UnityWebRequest hs_post = UnityWebRequest.Post(post_url, hash);

        yield return hs_post.SendWebRequest();
        if (hs_post.error != null)
            Debug.Log("There was an error posting the high score: "
                    + hs_post.error);
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
