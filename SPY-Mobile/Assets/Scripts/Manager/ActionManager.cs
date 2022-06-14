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
    

    #region Word Manager - part
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
    public void Login(GameObject Page)
    {
        SingeltonManager.Instance.canvasManager.Login(Page.name);
    }

    GameObject PageAdmin;

    public void DetermineUserObject1(GameObject PageAdmin)
    {
        this.PageAdmin = PageAdmin;
    }
    public void DetermineUserObject2(GameObject PagePlayer)
    {
        SingeltonManager.Instance.canvasManager.IsAdmin(PageAdmin.name, PagePlayer.name);
    }

    public void AcceptSuggestion()
    {
        SingeltonManager.Instance.canvasManager.AcceptSuggestion(gameObject.transform.parent.GetComponentInParent<IDGenerator>(), gameObject.transform.parent.parent.gameObject);
    }

    public void RejectSuggestion()
    {
        SingeltonManager.Instance.canvasManager.RejectSuggestion(gameObject.transform.parent.parent.gameObject, gameObject.transform.parent.GetComponentInParent<IDGenerator>());
    }

    public void RemoveFromPublicList()
    {
        SingeltonManager.Instance.canvasManager.RemoveFromPublicList();
    }

    public void SendSuggestion()
    {
        SingeltonManager.Instance.canvasManager.SendSuggestion();
    }

    #endregion

    #region Game - part

    public void NewPlayer()
    {
        SingeltonManager.Instance.canvasManager.NewPlayer();
    }

    public void FindAllList()
    {
        SingeltonManager.Instance.canvasManager.FindAllList();
    }

    public void ChooseTime(int BtnID)
    {
        SingeltonManager.Instance.canvasManager.ChooseTime(BtnID);
    }

    
    public void StartScoredGame(GameObject page)
    {
        SingeltonManager.Instance.canvasManager.StartScoredGame(page.name);
    }

    public void NextStepInGamePlay(GameObject page)
    {
        SingeltonManager.Instance.canvasManager.NextStepInGamePlay(page.name);
    }
    public void RoundEnd(int SpyScore)
    {
        SingeltonManager.Instance.canvasManager.RoundEnd(SpyScore);
    }

    public void NextRound(GameObject page)
    {
        SingeltonManager.Instance.canvasManager.NextRound(page.name);
    }

    #endregion
}
