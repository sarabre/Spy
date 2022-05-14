
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{

    [SerializeField] List<Panel> pages;

    [SerializeField] GameObject CurrentPage;

    [SerializeField] GameObject PersonalWordBtnPage;
    

    [SerializeField] ScrollRect BodyPersonalwordScroll;
    [SerializeField] RectTransform BodyPersonalwordRect;
    [SerializeField] RectTransform ContentPersonalword;
    [SerializeField] GridLayoutGroup ContentPersonalwordGrid;


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
        ResetPersonalWordPage(index);  
        GotoPage(PersonalWordBtnPage.name);
    }
    private void ResetPersonalWordPage(int index)
    {
        //Reset Scroll
        ContentPersonalword.sizeDelta = new Vector2(0, CalculateContentHeight(index));
        BodyPersonalwordScroll.verticalNormalizedPosition =1f;
        //create button
        SingeltonManager.Instance.poolManager.DisabledAllPersonalWordsBtn();
        SingeltonManager.Instance.poolManager.EnabledPersonalWordsBtn(index);
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
