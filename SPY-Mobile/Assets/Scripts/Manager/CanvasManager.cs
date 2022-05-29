
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

    [SerializeField] InputField AddedWord;
    [SerializeField] InputField DeletedWordIndex;

    [SerializeField] GameObject AlertBox;
    [SerializeField] Transform AlertPlace1;
    [SerializeField] Transform AlertPlace2;
    [SerializeField] RtlText AlertText;

    [SerializeField] InputField NewOrRemoveListNumber;

    [SerializeField] RtlText PersonalListBtnDetail;

    private int CurrentPersonalListIndex;
    private float ShowAlertTimer = 1f;
    private float NormalPositionScroll = 10;

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
    private float CalculateContentHeight(int index,bool IsListBtn)
    {
        if (IsListBtn)
        {
            float height = Convert.ToInt32(SingeltonManager.Instance.personalWordsManager.wordlist.Count/2 ) * ContentPersonaBtnGrid.cellSize.y;

            Debug.Log(height);
            Debug.Log("MinHeightScrollPersonalBtn  = " + MinHeightScrollPersonalBtn);

            if (height < MinHeightScrollPersonalBtn)
                return 0;
            else
                return (height - MinHeightScrollPersonalBtn + ContentPersonaBtnGrid.cellSize.y * 2.5f); // for reduce Numerical error


        }
        else
        {
            float height = (SingeltonManager.Instance.personalWordsManager.wordlist[index].Words.Count) * ContentPersonalwordGrid.cellSize.y;

            Debug.Log(height);
            Debug.Log("MinHeightScrollPersonalWord  = " + MinHeightScrollPersonalWord);

            if (height < MinHeightScrollPersonalWord)
                return 0;
            else
                return (height - MinHeightScrollPersonalWord * 1.7f);
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
        ResetScroolSize(0, ContentPersonalBTN, true);
        #endregion
    }
    public void ShowPersonalWord(int index ) //index list that the page show from Wordlist
    {
        CurrentPersonalListIndex = index;
        SingeltonManager.Instance.poolManager.UpdatePersonalWords(CurrentPersonalListIndex);
        TitlePersonalWord.text = SingeltonManager.Instance.poolManager.NamePersonalWordList(index);
        ResetScroolSize(CurrentPersonalListIndex, ContentPersonalword, false);
        GotoPage(PersonalWordBtnPage.name);
    }
   
    void ResetScroolSize(int index,RectTransform ScrollContent,bool IsList)
    {
        ScrollContent.sizeDelta = new Vector2(0, CalculateContentHeight(index,IsList));
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
        ResetScroolSize(CurrentPersonalListIndex, ContentPersonalword, false);

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
        ResetScroolSize(0, ContentPersonalBTN, true);
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
                ShowAlert(4004); // error for emphty name
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
        ResetScroolSize(0, ContentPersonalBTN, true);
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
        ResetScroolSize(CurrentPersonalListIndex, ContentPersonalword, false);

        #endregion
    }

    public void ShowPublicBtn()
    {
        SingeltonManager.Instance.poolManager.UpdatePublicList();
    }

    public void ShowPublicWords(int index)
    {
        SingeltonManager.Instance.poolManager.UpdatePublicWords(index);
        GotoPage(PublicWordBtnPage.name);
    }
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
