using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TalkButton : MonoBehaviour
{
    public static TalkButton instance;
    
    public GameObject button;
    public GameObject talkUI;


    private void Awake()
    {
        instance = this;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
            button.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
            button.SetActive(false);
    }

    private void Update()
    {
        // if (DoorOpen.instance != null)
        // {
        //     if(button.activeSelf == false && DoorOpen.instance.transitionButton.activeSelf == false)
        //         AndroidInputButton.instance.isClickedInteractiveButton = false;            
        // }
        // else
        // {
        //     if(button.activeSelf == false)
        //         AndroidInputButton.instance.isClickedInteractiveButton = false;    
        // }

        if (SavepositionButton.instance != null)
        {
            if(button.activeSelf == false && SavepositionButton.instance.button.activeSelf == false && DoorOpen.instance.transitionButton.activeSelf == false)
                AndroidInputButton.instance.isClickedInteractiveButton = false;               
        }
        else
        {
            if(button.activeSelf == false && DoorOpen.instance.transitionButton.activeSelf == false)
                AndroidInputButton.instance.isClickedInteractiveButton = false;       
        }


            
        if (button.activeSelf && (Input.GetKeyDown(KeyCode.E) || AndroidInputButton.instance.isClickedInteractiveButton))
        {
            talkUI.SetActive(true);
        }
    }
}