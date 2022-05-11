using UnityEngine;

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
    public void ChoosePersonalList()
    {
        SingeltonManager.Instance.canvasManager.ShowPersonalWord(ID);
    }
}
