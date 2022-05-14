using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = "Alerts-SO", menuName = "Alerts")]
public class Alerts : ScriptableObject
{
    public List<Alert> AlertList = new List<Alert>();

}

[Serializable]
[SerializeField]
public class Alert
{
    public int code;
    public string massage;
}
