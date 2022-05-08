using UnityEngine;

public class ActionManager : MonoBehaviour
{

    public void ChangePage(GameObject Page)
    {
        SingeltonManager.Instance.canvasManager.GotoPage(Page.name);
    }
}
