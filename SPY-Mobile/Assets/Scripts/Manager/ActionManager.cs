using UnityEngine;
using UPersian.Components;

public class ActionManager : MonoBehaviour
{
    int ID
    {
        get
        {
            return gameObject.GetComponent<IDGenerator>().ListID;
        }
    }

    public void ChangePage(GameObject Page)
    {
        SingeltonManager.Instance.canvasManager.GotoPage(Page.name);
    }
    public void ShowPersonalBtn()
    {
        SingeltonManager.Instance.canvasManager.ShowPersonalBtn();
    }
    public void ChoosePersonalList()
    {
        SingeltonManager.Instance.canvasManager.ShowPersonalWord(ID);
    }
    public void AddWordToPersonalWords()
    {
        SingeltonManager.Instance.canvasManager.NewPersonalWord();
    }
    public void DeleteWordFromPersonalWord()
    {
        SingeltonManager.Instance.canvasManager.RemovePersonalWord();
    }
     public void NewListBtn()
    {
        SingeltonManager.Instance.canvasManager.NewPersonalList();
    }
    public void DeleteListBtn()
    {
       SingeltonManager.Instance.canvasManager.RemovePersonalList();
    }

    public void ShowWordsList()
    {
        SingeltonManager.Instance.canvasManager.ShowWordsList();
    }

    public void ShowPublicBtn()
    {
        SingeltonManager.Instance.canvasManager.ShowPublicBtn();
    }

    public void ShowPublicWord()
    {
        SingeltonManager.Instance.canvasManager.ShowPublicWords(ID);
    }
}
