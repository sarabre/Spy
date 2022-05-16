using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingeltonManager : MonoBehaviour
{
    public CanvasManager canvasManager;
    public PoolManager poolManager;
    public PersonalWordsManager personalWordsManager;
    public AlertManager alertManager;

    private static SingeltonManager instance;

    public static SingeltonManager Instance
    {
        get
        {
            if(instance == null)
                instance = FindObjectOfType<SingeltonManager>();
            return instance;
        }
    }

    //pools
    public PersonalListPool personalListPool;
    public PersonalWordPool personalWordPool;
    

}