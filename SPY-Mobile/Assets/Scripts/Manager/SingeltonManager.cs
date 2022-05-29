using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingeltonManager : MonoBehaviour
{
   

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
    public PublicListPool publicListPool;
    public PublicWordPool publicWordPool;

    //controler
    public WordGroupController wordGroupControler;

    //Manager
    public CanvasManager canvasManager;
    public PoolManager poolManager;
    public PersonalWordsManager personalWordsManager;
    public AlertManager alertManager;
    public PublicWordsManager publicWordsManager;
}