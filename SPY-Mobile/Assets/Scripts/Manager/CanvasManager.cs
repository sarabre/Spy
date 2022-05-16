
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


    private int CurrentListIndex;
    private float ShowAlertTimer = 1f;

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
        ContentPersonalword.sizeDelta = new Vector2(0, CalculateContentHeight(index));
        BodyPersonalwordScroll.verticalNormalizedPosition =1f;
        //create button
        SingeltonManager.Instance.poolManager.DisabledAllPersonalWordsBtn();
        SingeltonManager.Instance.poolManager.EnabledPersonalWordsBtn(index);
    }

    public void AddWordToPersonalWords()
    {
        SingeltonManager.Instance.personalWordsManager.WordsList[CurrentListIndex].Words.Add(AddedWord.text);
        AddedWord.text = String.Empty;
        SingeltonManager.Instance.personalWordPool.AddObject(SingeltonManager.Instance.personalWordsManager.WordsList[CurrentListIndex].Words.Count, CurrentListIndex);


    }

    public void DeleteWordFromPersonalWord()
    {
        try
        {
            int RemoveIndex = int.Parse(DeletedWordIndex.text) - 1;
            SingeltonManager.Instance.personalWordsManager.WordsList[CurrentListIndex].Words.RemoveAt(RemoveIndex);
            Debug.Log("RemoveIndex = " + RemoveIndex);
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
