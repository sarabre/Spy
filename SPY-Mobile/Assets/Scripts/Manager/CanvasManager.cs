
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using UnityEngine.UI;
using UPersian.Components;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class CanvasManager : MonoBehaviour
{

    [SerializeField] List<Panel> pages;

    [SerializeField] GameObject CurrentPage;

    #region Word Manager - part


    [SerializeField] GameObject PersonalWordBtnPage;
    [SerializeField] GameObject PublicWordBtnPage;
    

    [SerializeField] ScrollRect BodyPersonalwordScroll;
    [SerializeField] ScrollRect BodyPersonalBtnScroll;
    [SerializeField] RectTransform BodyPersonalwordRect;
    [SerializeField] RectTransform BodyPersonalBtnRect;
    [SerializeField] RectTransform ContentPersonalword;
    [SerializeField] RectTransform ContentPersonalBTN;
    [SerializeField] GridLayoutGroup ContentPersonalwordGrid;
    [SerializeField] GridLayoutGroup ContentPersonaBtnGrid;
    [SerializeField] RtlText TitlePersonalWord;

    [SerializeField] ScrollRect BodyPublicwordScroll;
    [SerializeField] ScrollRect BodyPublicBtnScroll;
    [SerializeField] RectTransform BodyPublicwordRect;
    [SerializeField] RectTransform BodyPublicBtnRect;
    [SerializeField] RectTransform ContentPublicword;
    [SerializeField] RectTransform ContentPublicBTN;
    [SerializeField] GridLayoutGroup ContentPublicwordGrid;
    [SerializeField] GridLayoutGroup ContentPublicBtnGrid;
    [SerializeField] RtlText TitlePubliclWord;

    [SerializeField] InputField AddedWord;
    [SerializeField] InputField DeletedWordIndex; //private

    [SerializeField] GameObject AlertBox;
    [SerializeField] Transform AlertPlace1;
    [SerializeField] Transform AlertPlace2;
    [SerializeField] RtlText AlertText;

    [SerializeField] InputField NewOrRemoveListNumber;

    [SerializeField] RtlText PersonalListBtnDetail;

    [SerializeField] InputField UserName;
    [SerializeField] InputField PassWord;

    [SerializeField] GameObject SuggestedToList;
    [SerializeField] GameObject DeleteFromPublicList;

    [SerializeField] InputField DeletedPublicWordIndex;
    [SerializeField] InputField NewWordInSuggestion;

    [SerializeField] Text AdminWelcome;
    public string Welcome;

    private int CurrentPersonalListIndex;
    private float ShowAlertTimer = 1f;
    
    private int CurrentPubliclListIndex;

    private float NormalPositionScroll = 1f;

    private ListContent NewListContent = new ListContent();

    float MinHeightScrollPersonalWord
    {
        get
        {
             return BodyPersonalwordRect.rect.height;
        }
    }

    float MinHeightScrollPersonalBtn
    {
        get
        {
            return BodyPersonalBtnRect.rect.height;
        }
    }

    float MinHeightScrollPublicWord
    {
        get
        {
            return BodyPublicwordRect.rect.height;
        }
    }

    float MinHeightScrollPublicBtn
    {
        get
        {
            return BodyPublicBtnRect.rect.height;
        }
    }


    private float CalculateContentHeightPersonal(int index,bool IsListBtn,int BtnCount,float MinHeightBtn , float BtnY, int WordCount, float MinHeightWord, float WordY)
    {
        if (IsListBtn)
        {
            float height = BtnCount * BtnY;


            if (height < MinHeightBtn)
                return 0;
            else
                return (height - MinHeightBtn + BtnY * 2.5f); // for reduce Numerical error


        }
        else
        {
            float height = WordCount * WordY;


            if (height < MinHeightWord)
                return 0;
            else
                return (height - MinHeightWord * 1.7f);
        }
       
        
    }

    public void GotoPage(string name)
    {
        CurrentPage.SetActive(false);
        CurrentPage = pages[FindPageIndex(name)].Page;
        CurrentPage.SetActive(true);   
    }

    private int FindPageIndex(string name)
    {
        for (int i = 0; i < pages.Count ; i++)
        {
            if (pages[i].Name == name)
                return i;       
        }
        return 0;
    }

    public void ShowPersonalBtn() 
    {
        #region Update list
        SingeltonManager.Instance.poolManager.UpdatePersonalList();

        #endregion

        #region Update scroll

        BodyPersonalBtnScroll.verticalNormalizedPosition = NormalPositionScroll;
        ResetScroolSize(0, ContentPersonalBTN, true,false);
        #endregion
    }
    public void ShowPersonalWord(int index ) //index list that the page show from Wordlist
    {
        CurrentPersonalListIndex = index;
        SingeltonManager.Instance.poolManager.UpdatePersonalWords(CurrentPersonalListIndex);
        TitlePersonalWord.text = SingeltonManager.Instance.poolManager.NamePersonalWordList(index);
        ResetScroolSize(CurrentPersonalListIndex, ContentPersonalword, false,false);
        GotoPage(PersonalWordBtnPage.name);
    }
   
    void ResetScroolSize(int index,RectTransform ScrollContent,bool IsList,bool IsPublic)
    {
        Debug.Log("IsPublic  = " + IsPublic);
        if(!IsPublic)
            ScrollContent.sizeDelta = new Vector2(0, CalculateContentHeightPersonal(index,IsList, Convert.ToInt32(SingeltonManager.Instance.personalWordsManager.wordlist.Count / 2), MinHeightScrollPersonalBtn, ContentPersonaBtnGrid.cellSize.y, SingeltonManager.Instance.personalWordsManager.wordlist[index].Words.Count, MinHeightScrollPersonalWord, ContentPersonalwordGrid.cellSize.y));
        else
            ScrollContent.sizeDelta = new Vector2(0, CalculateContentHeightPersonal(index,IsList, SingeltonManager.Instance.wordGroupControler.TablesNameFromDataBase.Count , MinHeightScrollPublicBtn, ContentPublicBtnGrid.cellSize.y, SingeltonManager.Instance.wordGroupControler.Tables[index].Word.Count, MinHeightScrollPublicWord, ContentPublicwordGrid.cellSize.y));
    }

    public void ShowAlert(int code)
    {
        AlertText.text = SingeltonManager.Instance.alertManager.FindAlertByCode(code);   
        StartCoroutine(AlertMovement());
    }

    IEnumerator AlertMovement()
    {
        AlertBox.transform.DOMoveY(AlertPlace2.position.y, ShowAlertTimer);
        yield return new WaitForSeconds(4f);
        AlertBox.transform.DOMoveY(AlertPlace1.position.y, ShowAlertTimer);
    }

    public void MakeInputEmpty(InputField input)
    {
        input.text = String.Empty;
    }

    public void ShowWordsList()
    {
        try
        {
            switch (SingeltonManager.Instance.personalWordsManager.wordlist.Count)
            {
                case 0:
                    PersonalListBtnDetail.text = " ";
                    break;
                case 1:
                    PersonalListBtnDetail.text = $"{SingeltonManager.Instance.personalWordsManager.wordlist[0].ListName} ";
                    break;
                case 2:
                    PersonalListBtnDetail.text = $"{SingeltonManager.Instance.personalWordsManager.wordlist[0].ListName} , {SingeltonManager.Instance.personalWordsManager.wordlist[1].ListName} ";
                    break;
                default:
                    PersonalListBtnDetail.text = $"{SingeltonManager.Instance.personalWordsManager.wordlist[0].ListName} , {SingeltonManager.Instance.personalWordsManager.wordlist[1].ListName} , {SingeltonManager.Instance.personalWordsManager.wordlist[2].ListName} , ...";
                    break;
            }
       
        }
        catch
        {

        }
    }
    
    public void NewPersonalWord()
    {
        #region Call Manager

        SingeltonManager.Instance.personalWordsManager.AddWord(CurrentPersonalListIndex ,AddedWord.text);

        #endregion

        #region Update list

        SingeltonManager.Instance.poolManager.UpdatePersonalWords(CurrentPersonalListIndex);
        MakeInputEmpty(AddedWord);

        #endregion

        #region Update scroll and page

        BodyPersonalwordScroll.verticalNormalizedPosition = NormalPositionScroll;
        ResetScroolSize(CurrentPersonalListIndex, ContentPersonalword, false,false);

        #endregion
    }

    public void NewPersonalList()
    {
        #region Call manager
        if ( SingeltonManager.Instance.personalWordsManager.wordlist.Count < 12 ) 
        {
            if(NewOrRemoveListNumber.text != String.Empty)
            SingeltonManager.Instance.personalWordsManager.AddGroup(NewOrRemoveListNumber.text);
            else
            {
                ShowAlert(4003); // error for emphty name
                return;
                
            }
        }
        else
        {
            ShowAlert(4002); // error for more than 12
            return;
        }
        #endregion

        #region Update list
        SingeltonManager.Instance.poolManager.UpdatePersonalList();
        MakeInputEmpty(NewOrRemoveListNumber);
        #endregion

        #region Update scroll

        BodyPersonalBtnScroll.verticalNormalizedPosition = NormalPositionScroll;
        ResetScroolSize(0, ContentPersonalBTN, true,false);
        #endregion
    }

    public void RemovePersonalList()
    {
        #region Call manager
        
        try
        {
            if (NewOrRemoveListNumber.text != String.Empty)
                SingeltonManager.Instance.personalWordsManager.RemoveGroup(int.Parse(NewOrRemoveListNumber.text)-1);
            else
            {
                ShowAlert(4003); // error for emphty name
                return;

            }
        }
        catch
        {
            ShowAlert(4001); // no this number in list
            return;
        }
        #endregion

        #region Update list
        SingeltonManager.Instance.poolManager.UpdatePersonalList();
        MakeInputEmpty(NewOrRemoveListNumber);
        #endregion

        #region Update scroll

        BodyPersonalBtnScroll.verticalNormalizedPosition = NormalPositionScroll;
        ResetScroolSize(0, ContentPersonalBTN, true,false);
        #endregion
    }

    public void RemovePersonalWord()
    {
        #region Call Manager
        try
        {
           SingeltonManager.Instance.personalWordsManager.RemoveWord(CurrentPersonalListIndex, int.Parse(DeletedWordIndex.text)-1);
        }
        catch
        {
            ShowAlert(4001); // no this number in list
        }

        #endregion

        #region Update list

        SingeltonManager.Instance.poolManager.UpdatePersonalWords(CurrentPersonalListIndex);
        MakeInputEmpty(DeletedWordIndex);

        #endregion

        #region Update scroll and page

        BodyPersonalwordScroll.verticalNormalizedPosition = NormalPositionScroll;
        ResetScroolSize(CurrentPersonalListIndex, ContentPersonalword, false,false);

        #endregion
    }

    public void ShowPublicBtn()
    {
        SingeltonManager.Instance.poolManager.UpdatePublicList();
    }

    public void ShowPublicWords(int index)
    {
        CurrentPubliclListIndex = index;
        SingeltonManager.Instance.poolManager.UpdatePublicWords(index);
        TitlePubliclWord.text = SingeltonManager.Instance.poolManager.NamePublicWordList(index);
        BodyPublicwordScroll.verticalNormalizedPosition = NormalPositionScroll;
        ResetScroolSize(index, ContentPublicword, false,true);
        ShowIfAdmin((SingeltonManager.Instance.profile.UserSit == UserSituation.Admin));
        GotoPage(PublicWordBtnPage.name);
    }

   
    public void ShowIfAdmin(bool IsAdmin)
    {
        
            SuggestedToList.SetActive(!IsAdmin);
            DeleteFromPublicList.SetActive(IsAdmin);
       
    }

    public void Login(string PageName)
    {
        
       
        StartCoroutine(DetermineUserSituation(PageName));
        
    }
    // 4007 no internet
    IEnumerator DetermineUserSituation(string PageName)
    {
        SingeltonManager.Instance.wordGroupControler.CheckAdmin(UserName.text,PassWord.text);

        if (SingeltonManager.Instance.profile.UserSit == UserSituation.Unknown)
            ShowAlert(4006); // wait for some seconde

        yield return new WaitForSeconds(1f);

        if (SingeltonManager.Instance.profile.UserSit == UserSituation.Player)
            ShowAlert(4008); // you are not admin

        if (SingeltonManager.Instance.profile.UserSit == UserSituation.Admin)
        {
            WelcomeAdmin();
            SingeltonManager.Instance.suggestionManager.GetSuggestion();
            
            yield return new WaitForSeconds(2f);
            
            GotoPage(PageName);
        }
        
    }

    public void WelcomeAdmin()
    {
        AdminWelcome.text = (SingeltonManager.Instance.profile.name + Welcome);
    }

    public void IsAdmin(string PageAdmin, string PagePlayer)
    {
        if (SingeltonManager.Instance.profile.UserSit == UserSituation.Admin)
            GotoPage(PageAdmin);
        else
            GotoPage(PagePlayer);
    }

    public void AcceptSuggestion(IDGenerator Info,GameObject tmp)
    {
        SingeltonManager.Instance.wordGroupControler.AddWord(Info.word, Info.WordGroupCode);
        tmp.SetActive(false);
    }

    public void RejectSuggestion(GameObject tmp, IDGenerator Info)
    {
        SingeltonManager.Instance.wordGroupControler.RemoveSuggestion(Info.word);
        tmp.SetActive(false);
    }
    public void RemoveFromPublicList()
    {
        try
        {
            SingeltonManager.Instance.publicWordsManager.RemoveWord(int.Parse(DeletedPublicWordIndex.text)-1, CurrentPubliclListIndex);

            ShowAlert(4009); // For sea the correct word and situastion plz reload the game
            MakeInputEmpty(DeletedPublicWordIndex);
        }
        catch
        {
            ShowAlert(4010); //rong name
        }

    }

    public void SendSuggestion()
    {
        
        SingeltonManager.Instance.suggestionManager.SendSuggestion(NewWordInSuggestion.text, SingeltonManager.Instance.publicWordsManager.GetWordGroupCode(Convert.ToInt32(CurrentPubliclListIndex+1)));
        ShowAlert(4004); //suggestion succesed
        MakeInputEmpty(NewWordInSuggestion);
    }

    #endregion

    #region Game - part


    [SerializeField] GameObject Player;
    [SerializeField] Transform PlayerFather; // Content in grid
    [SerializeField] List<GameObject> Players;

    [SerializeField] Dropdown WordGroupListScored;
    [SerializeField] Dropdown WordGroupListNormal;
    [SerializeField] Dropdown RoundNumber;
    [SerializeField] Dropdown SpyNumberDp;

    //Normal
    [SerializeField] Dropdown SpyNumberDpNormalGame;
    [SerializeField] Dropdown PlayerNumberDpNormalGame;
    [SerializeField] Dropdown RoundNumberDpNormalGame;

    float Time;
    int PossibleSpyNumber;
    List<string> TableName = new List<string>();

    public string AllTableItem;
    public string JustPublicTableItem;
    public string JustPersonalTableItem;

    [SerializeField] List<GameObject> ScoredTimers;
    [SerializeField] List<GameObject> NormalTimers;
    [SerializeField] Color ChooseBtnColor;
    [SerializeField] Color ChooseTextColor;
    [SerializeField] Color TextColor;

    [SerializeField] RtlText GamePlayText;
    [SerializeField] RtlText GamePlayBtn;
    [SerializeField] RtlText ScoredGamePlayTimer;
    [SerializeField] RtlText NormalGamePlayTimer;
    [SerializeField] GameObject WinerPlayers;
    [SerializeField] RtlText NumberOfRound;
    [SerializeField] RtlText NextRoundOrEnd;

    public string GivePhone;              //
    public string GivePhoneTo;            //
    public string Spy;                    //  For Persian writing
    public string Iam;                    //
    public string Next;                   //
    public string Player1;                //
    public string Start;                  //
    public string ResultOfRound;          //
    public string Results;                //
    public string HomePage;               //
    public string NextOne;                //

    private bool IsLastRound;
    private bool IsIAm = true;
    private bool IsLastPlayer = false;
    private int MaxScore;
    private List<string> MaxScorePlayerName = new List<string>();

    Player player = new Player();

    [SerializeField] GameObject TablePanel;
    [SerializeField] GameObject RowPlayer;
    [SerializeField] Transform RowPlayerFather;
    [SerializeField] List<GameObject> RowPlayers = new List<GameObject>();

    public Coroutine GamePlayTimerCoroutinr;
    public Coroutine GamePlayNormalTimerCoroutinr;

    public void NewPlayer()
    {
        if ( Players.Count < 8)
        {
            if(Players.Count == 0 || Players[Players.Count-1].GetComponent<InputField>().text != String.Empty ) //user should type the name before add new player
            {
                GameObject tmp = Instantiate(Player, PlayerFather);
                Players.Add(tmp);
                DetermineSpyNumber();
            }
            else
            {
                ShowAlert(4011); // Input Player name
            }
        }
        else
            ShowAlert(4005); // not more than 8 player
       
    }

        Dropdown WordGroupList;
    public void FindAllList(bool IsScored)
    {

        SortWordGroupList();

        if (IsScored)
            WordGroupList = WordGroupListScored;
        else
            WordGroupList = WordGroupListNormal;
            

        WordGroupList.options.Clear();

        foreach (string item in TableName)
        {
            WordGroupList.options.Add(new Dropdown.OptionData() { text = item });
        }
    }

    public void SortWordGroupList()
    {
        TableName.Clear();

        TableName.Add(AllTableItem);
        TableName.Add(JustPublicTableItem);
        TableName.Add(JustPersonalTableItem);

        SingeltonManager.Instance.publicWordsManager.GetAllPublicTable(ref TableName);
        SingeltonManager.Instance.personalWordsManager.GetAllPersonalTable(ref TableName);
    }

    public void ChooseTime(int index,bool IsScord)
    {
        if (IsScord)
        {
            //make all black
            foreach (var item in ScoredTimers)
            {
                item.GetComponent<Image>().color = Color.white;
                item.GetComponentInChildren<RtlText>().color = TextColor;

            }
            //click 
            ScoredTimers[index].GetComponent<Image>().color = ChooseBtnColor;
            ScoredTimers[index].GetComponentInChildren<RtlText>().color = ChooseTextColor;
        }
        else
        {
            //make all black
            foreach (var item in NormalTimers)
            {
                item.GetComponent<Image>().color = Color.white;
                item.GetComponentInChildren<RtlText>().color = TextColor;

            }
            //click 
            NormalTimers[index].GetComponent<Image>().color = ChooseBtnColor;
            NormalTimers[index].GetComponentInChildren<RtlText>().color = ChooseTextColor;
        }

        Time = index * 60 + 120;
    }

    public void DetermineSpyNumber()
    {
        PossibleSpyNumber++;
        
        if (PossibleSpyNumber > 3)
        {
            SpyNumberDp.options.Add(new Dropdown.OptionData() { text = (PossibleSpyNumber-2).ToString() });
        }
        
        
    }

    public void StartScoredGame(string page)
    {
        try
        {

            #region send round

            int Round = int.Parse(RoundNumber.options[RoundNumber.value].text);
            if (Round == 0)
            {
                ShowAlert(4014);
                return;
            }
            else
                SingeltonManager.Instance.team.NumberOfRound = Round;
            #endregion

            #region send word


            if (WordGroupListScored.value != 0)
                SingeltonManager.Instance.GameManager.MakeListOfWord(WordGroupListScored.value);
            else
                SingeltonManager.Instance.GameManager.MakeListOfWord(0);

            if (SingeltonManager.Instance.GameManager.Words.Count < Round)
            {
                ShowAlert(4016); //words count is less than round
                return;
            }

            #endregion

            #region check spy number  and send


            int spynumber = SpyNumberDp.value + 1;
            if (spynumber == 0)
            {
                ShowAlert(4012); //choose spy number
                return;
            }
            else
                SingeltonManager.Instance.team.NumberOfSpy = spynumber;

            #endregion

            #region send Time

            if (Time == 0)
            {
                ShowAlert(4013); // choose time
                return;
            }
            else
                SingeltonManager.Instance.team.RoundDuration = Time;
            #endregion

            #region Send Player

            if (PlayerFather.childCount <= 2)
            {
                ShowAlert(4016); // more than 2 player
                return;
            }
            else
            {
                if (SingeltonManager.Instance.team.players.Count != 0) //if information change after error in catch
                    SingeltonManager.Instance.team.players.Clear();

                foreach (var item in PlayerFather.GetComponentsInChildren<InputField>())
                {
                    if (item.text != string.Empty)
                        SingeltonManager.Instance.team.AddPlayer(item.text);
                    else
                        SingeltonManager.Instance.team.AddPlayer(Player1);

                }
            }
                
            #endregion



            SingeltonManager.Instance.GameManager.ManageGame();

            GotoPage(page);

        }
        catch
        {
            ShowAlert(4015); //rong information
        }

        


    }

    public void StartNormalGame(string page)
    {
        try
        {
            #region check player number and spy

            int spynumber = SpyNumberDpNormalGame.value + 1;
            int playernumber = PlayerNumberDpNormalGame.value + 3;

            if (playernumber< spynumber + 2)
            {
                ShowAlert(4018); //player should be 2 + spy or more
                return;
            }
            else
            {
                SingeltonManager.Instance.team.NumberOfSpy = spynumber;
                SingeltonManager.Instance.team.NumberOfPlayer = playernumber;
            }

            #endregion

            #region send round

            int Round = int.Parse(RoundNumberDpNormalGame.options[RoundNumberDpNormalGame.value].text);
            if (Round == 0)
            {
                ShowAlert(4014);
                return;
            }
            else
                SingeltonManager.Instance.team.NumberOfRound = Round;

            #endregion

            #region send word
            if (WordGroupListNormal.value != 0)
                SingeltonManager.Instance.GameManager.MakeListOfWord(WordGroupListNormal.value);
            else
                SingeltonManager.Instance.GameManager.MakeListOfWord(0);

            if (SingeltonManager.Instance.GameManager.Words.Count < Round)
            {
                ShowAlert(4016); //words count is less than round
                return;
            }
            #endregion

            #region send time

            if (Time == 0)
            {
                ShowAlert(4013); // choose time
                return;
            }
            else
                SingeltonManager.Instance.team.RoundDuration = Time;
            #endregion


            SingeltonManager.Instance.GameManager.ManageNormalGame();

            GotoPage(page);

        }
        catch
        {
            ShowAlert(4015); //rong information
        }
    }
    public void DeterminePlayerName(string name)
    {
        if(name == "next one")
        GamePlayText.text = $"{GivePhoneTo} {NextOne} {GivePhone}.";
        else
        GamePlayText.text = $"{GivePhoneTo} {name} {GivePhone}.";

        GamePlayBtn.text = Iam;
    }

    public void ShowPlayerRole(bool IsSpy , string word,bool IsLastPlayer)
    {
        if (IsSpy)
        {
            GamePlayText.text = $"{Spy}";
        }
        else
        {
            GamePlayText.text = $"{word}";
        }

        if(!IsLastPlayer)
            GamePlayBtn.text = Next;
        else
            GamePlayBtn.text = Start;

        this.IsLastPlayer = IsLastPlayer;
    }
    public void NextStepInGamePlay(string page,bool IsScored) //Next Btn
    {
        if(IsLastPlayer)
        {
            GotoPage(page);
            if(IsScored)
                GamePlayTimerCoroutinr = StartCoroutine(SingeltonManager.Instance.GameManager.ManagingTime(true));
            else
                GamePlayNormalTimerCoroutinr = StartCoroutine(SingeltonManager.Instance.GameManager.ManagingTime(false));
            return;
        }

        if(IsIAm)
            SingeltonManager.Instance.GameManager.ImInGamePlay();
        else
            SingeltonManager.Instance.GameManager.GivePhoneToPlayer(IsScored);
        IsIAm = !IsIAm;
    }

    public void Timer(int min,int second,bool IsScored)
    {
        if(IsScored)
            ScoredGamePlayTimer.text = $"{min.ToString()} : {second.ToString()}";
        else
            NormalGamePlayTimer.text = $"{min.ToString()} : {second.ToString()}";
 
    }

    public void RoundEnd(int SpyScore)
    {
        StopCoroutine(GamePlayTimerCoroutinr);
        SingeltonManager.Instance.GameManager.GiveResultOfThisRound(SpyScore);
        ShowScoredInTable();
        GotoPage(TablePanel.name);
    }
    public void ShowScoredInTable()
    {
        NumberOfRound.text = $"{ResultOfRound} {SingeltonManager.Instance.GameManager.PlayedRound}";


        MaxScorePlayerName.Clear();
        MaxScore = 0;
        int i = 0;

        foreach (var player in RowPlayers)
        {

            if (MaxScore == SingeltonManager.Instance.team.players[i].Score)
            {
                MaxScorePlayerName.Add(SingeltonManager.Instance.team.players[i].name);
            }

            if (SingeltonManager.Instance.team.players[i].Score > MaxScore)
            {
                MaxScore = SingeltonManager.Instance.team.players[i].Score;
                MaxScorePlayerName.Clear();
                MaxScorePlayerName.Add(SingeltonManager.Instance.team.players[i].name);
            }
            player.transform.GetChild(1).GetComponent<RtlText>().text = SingeltonManager.Instance.team.players[i].Score.ToString();
            i++;
        }

        foreach (var item in WinerPlayers.transform.GetComponentsInChildren<RtlText>()) //make empthy
        {
            item.text = "";
        }

        int j = 0;
        foreach (var PlayerName in MaxScorePlayerName)
        {
            WinerPlayers.transform.GetChild(j).GetComponent<RtlText>().text = PlayerName;
            j++;
        }

        if (SingeltonManager.Instance.GameManager.WasLastRound())
        {
            NextRoundOrEnd.text = HomePage;
            NumberOfRound.text = Results;
            IsLastRound = true;
        }


    }

    public void CreateTableRow()
    {
        for (int i = 0; i < SingeltonManager.Instance.team.NumberOfPlayer ; i++)
        {
            GameObject tmp = Instantiate(RowPlayer, RowPlayerFather);
            tmp.transform.GetChild(0).GetComponent<RtlText>().text = SingeltonManager.Instance.team.players[i].name;
            RowPlayers.Add(tmp);
        }
       
    }

    public void NextRound(string page,bool IsScored)
    {
        if (!IsLastRound)
        {
            SingeltonManager.Instance.GameManager.NextRound(IsScored);
            IsLastPlayer = false;
            IsIAm = !IsIAm;
            GotoPage(page);
        }
        else
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

    }

    public void StopNormalGame()
    {
        StopCoroutine(GamePlayNormalTimerCoroutinr);
    }

    #endregion
}


[Serializable]
[SerializeField]
public class Panel
{
    public string Name
    {
        get
        {
            if (Page != null)
                return Page.gameObject.name;
            else return null ;
        }
    }
    public GameObject Page;
    
}
