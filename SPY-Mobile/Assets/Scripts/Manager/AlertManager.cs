using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AlertManager : MonoBehaviour
{
    private List<Alert> AlertList = new List<Alert>();

    public List<Alert> alertlist
    {
        get{
            return AlertList;
        }
    }

    public void Awake()
    {
        Alerts listPersonalWords = Resources.Load<Alerts>("Alerts-SO");
        if (listPersonalWords != null)
        {
            AlertList = listPersonalWords.AlertList;
        }
    }

    public string FindAlertByCode(int code)
    {
        for (int i = 0; i < alertlist.Count; i++)
        {
            if (code == alertlist[i].code)
                return alertlist[i].massage;
                
        }
        return "error ";
    }

    
}
