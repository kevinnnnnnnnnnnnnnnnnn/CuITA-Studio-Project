using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AndroidInputButton : MonoBehaviour
{
    public static AndroidInputButton instance;

    public bool isClickedJumpButton;
    public bool isClickedInteractiveButton;
    
    private void Awake()
    {
        instance = this;
    }
    
    
    public void OnClickedJumpButton()
    {
        isClickedJumpButton = true;
    }

    
    public void OnClickedInteractiveButton()
    {
        isClickedInteractiveButton = true;
    }
}

