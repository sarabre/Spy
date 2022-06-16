using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Profile : MonoBehaviour
{
    

    public UserSituation UserSit;

    public string name;
    public string UserName;
    
}
public enum UserSituation
{

    Unknown,
    Admin,
    Player

};