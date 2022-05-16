
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
    

    [SerializeField] ScrollRect BodyPersonalwordScroll;
    [SerializeField] RectTransform BodyPersonalwordRect;
    [SerializeField] RectTransform ContentPersonalword;
    [SerializeField] GridLayoutGroup ContentPersonalwordGrid;
    [SerializeField] RtlText TitlePersonalWord;

    [SerializeField] InputField AddedWord;
    [SerializeField] InputField DeletedWordIndex;

    [SerializeField] GameObject AlertBox;
    [SerializeField] Transform AlertPlace1;
    [SerializeField] Transform AlertPlace2;
    [SerializeField] RtlText AlertText;

    [SerializeField] InputField NewOrRemoveListNumber;

    private int CurrentListIndex;
    private float ShowAlertTimer = 1f;

    private ListContent NewListContent = new ListContent();

    float MinHeightScrollPersonal
    {
        get
        {
             return BodyPersonalwordRect.rect.height;
        }
    }
    private float CalculateContentHeight(int index)
    {
        
        float height = (SingeltonManager.Instance.personalWordPool.pooledObjects[index].wordGroup.Count) * ContentPersonalwordGrid.cellSize.y;
        

        if (height < MinHeightScrollPersonal)
            return 0;
        else
            return (height - MinHeightScrollPersonal*2);
        
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

    public void ShowPersonalWord(int index ) //index list
    {
        CurrentListIndex = index;
        ResetPersonalWordPage(index);  
        GotoPage(PersonalWordBtnPage.name);
    }
    private void ResetPersonalWordPage(int index)
    {
        //change title
        TitlePersonalWord.text = SingeltonManager.Instance.poolManager.NameWordList(index);
        //Reset Scroll
        ResetScroolSize(index);
        BodyPersonalwordScroll.verticalNormalizedPosition = 1f;
        //create button
        SingeltonManager.Instance.poolManager.DisabledAllPersonalWordsBtn();
        SingeltonManager.Instance.poolManager.EnabledPersonalWordsBtn(index);
    }

    void ResetScroolSize(int index)
    {
        ContentPersonalword.sizeDelta = new Vector2(0, CalculateContentHeight(index));
        
    }

    public void AddWordToPersonalWords()
    {
        SingeltonManager.Instance.personalWordsManager.WordsList[CurrentListIndex].Words.Add(AddedWord.text);
        AddedWord.text = String.Empty;
        SingeltonManager.Instance.personalWordPool.AddObject(SingeltonManager.Instance.personalWordsManager.WordsList[CurrentListIndex].Words.Count, CurrentListIndex);
        ResetScroolSize(CurrentListIndex);

    }

    public void DeleteWordFromPersonalWord()
    {
        try
        {
            int RemoveIndex = int.Parse(DeletedWordIndex.text) - 1;
            SingeltonManager.Instance.personalWordsManager.WordsList[CurrentListIndex].Words.RemoveAt(RemoveIndex);
            SingeltonManager.Instance.personalWordsManager.WordsList[CurrentListIndex].Words.RemoveAt(RemoveIndex);
            SingeltonManager.Instance.personalWordPool.RemoveObject(RemoveIndex, CurrentListIndex);
            SingeltonManager.Instance.poolManager.UpdateListIndexAfterRemove(RemoveIndex ,CurrentListIndex);
        }
        catch
        {
            ShowAlert(4001);
        }
        DeletedWordIndex.text = String.Empty;
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

    public void NewList()
    {
        NewListContent.ListName = NewOrRemoveListNumber.text;
        SingeltonManager.Instance.personalWordsManager.WordsList.Add(NewListContent);
        SingeltonManager.Instance.personalListPool.AddBtn(SingeltonManager.Instance.personalWordsManager.WordsList.Count);
        NewListContent = new ListContent();
        NewOrRemoveListNumber.text = String.Empty;
    }
    public void RemoveList()
    {
        try
        {
            int index = int.Parse(NewOrRemoveListNumber.text)-1;
            SingeltonManager.Instance.personalWordsManager.WordsList.RemoveAt(index);
            SingeltonManager.Instance.personalListPool.RemoveBtn(index);
            SingeltonManager.Instance.poolManager.UpdateListBtn(index);
        }
        catch
        {
            ShowAlert(4002);
        }
            NewOrRemoveListNumber.text = String.Empty;
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
