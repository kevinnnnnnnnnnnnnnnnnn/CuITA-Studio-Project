using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject playerNew;
    
    
    private void Awake()
    {
        if(instance != null)
            Destroy(instance);
        instance = this;
    }
    
    
    
}
