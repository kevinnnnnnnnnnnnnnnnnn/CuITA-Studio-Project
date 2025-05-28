using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherSceneTransition : MonoBehaviour
{
    public Vector2 targetPosition;
    public string fromSceneName;
    public string toSceneName;
    
        
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OtherSceneTransitionManager.instance.TransitionOtherScene(fromSceneName, toSceneName, targetPosition);
        }
    }
}
