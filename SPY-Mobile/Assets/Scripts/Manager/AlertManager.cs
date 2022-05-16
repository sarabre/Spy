using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AlertManager : MonoBehaviour
{
    public List<Alert> AlertList = new List<Alert>();


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
        for (int i = 0; i < AlertList.Count; i++)
        {
            if (code == AlertList[i].code)
                return AlertList[i].massage;
                
        }
        return "error ";
    }

    
}
