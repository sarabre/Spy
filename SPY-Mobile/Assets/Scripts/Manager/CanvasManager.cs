
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using UnityEngine.UI;
using UPersian.Components;
using DG.Tweening;
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

    [SerializeField] Dropdown WordGroupList;
    [SerializeField] Dropdown RoundNumber;
    [SerializeField] Dropdown SpyNumberDp;

    float Time;
    int PossibleSpyNumber;
    List<string> TableName = new List<string>();

    public string AllTableItem;
    public string JustPublicTableItem;
    public string JustPersonalTableItem;

    [SerializeField] List<GameObject> Timers;
    [SerializeField] Color ChooseBtnColor;
    [SerializeField] Color ChooseTextColor;
    [SerializeField] Color TextColor;

    Player player = new Player();

    public void NewPlayer()
    {
        if ( Players.Count < 8)
        {
            if(Players.Count == 0 || Players[Players.Count-1].GetComponent<InputField>().text != String.Empty ) //user should type the name before add new player
            {
                GameObject tmp = Instantiate(Player, PlayerFather);
                Players.Add(tmp);
            }
            else
            {
                ShowAlert(4011); // Input Player name
            }
        }
        else
            ShowAlert(4005); // not more than 8 player
       
    }

    public void FindAllList()
    {

        SortWordGroupList();

        foreach (string item in TableName)
        {
            WordGroupList.options.Add(new Dropdown.OptionData() { text = item });
        }
    }

    public void SortWordGroupList()
    {
        WordGroupList.options.Clear();

        TableName.Add(AllTableItem);
        TableName.Add(JustPublicTableItem);
        TableName.Add(JustPersonalTableItem);

        SingeltonManager.Instance.personalWordsManager.GetAllPersonalTable(ref TableName);
        SingeltonManager.Instance.publicWordsManager.GetAllPublicTable(ref TableName);
    }

    public void ChooseTime(int index)
    {
        //make all black
        foreach (var item in Timers)
        {
            item.GetComponent<Image>().color = Color.white;
            item.GetComponentInChildren<RtlText>().color = TextColor;

        }
        //click 
        Timers[index].GetComponent<Image>().color = ChooseBtnColor;
        Timers[index].GetComponentInChildren<RtlText>().color = ChooseTextColor;
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
        //try
       // {
            #region check spy number  and send

        
            int spynumber = int.Parse(SpyNumberDp.options[SpyNumberDp.value].text);
            if (spynumber == 0)
            {
                ShowAlert(4012); //choose spy number
                return;
            }
            else
                SingeltonManager.Instance.team.NumberOfSpy = spynumber;

            #endregion

            #region Send Player

            if (PlayerFather.childCount <= 2)
            {
                ShowAlert(4016); // more than 2 player
                return;
            }
            else
                foreach (var item in PlayerFather.GetComponentsInChildren<InputField>())
                {
                    if (item.text != string.Empty)
                        SingeltonManager.Instance.team.AddPlayer(item.text);
                    else
                        SingeltonManager.Instance.team.AddPlayer(item.placeholder.ToString());

                }
            #endregion

            #region send round

            int Round = int.Parse(RoundNumber.options[RoundNumber.value].text);
            if (Round == 0)
            {
                ShowAlert(4014);
                return;
            }
            else
                SingeltonManager.Instance.team.NumberOfSpy = Round;
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

            #region send word

            SingeltonManager.Instance.GameManager.MakeListOfWord(WordGroupList.value);

            #endregion

            GotoPage(page);
       // }
       // catch
       // {
       //     ShowAlert(4015); //rong information
       // }




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
