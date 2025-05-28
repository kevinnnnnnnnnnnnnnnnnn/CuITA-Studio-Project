using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkButtonCUSGA : MonoBehaviour
{
    public GameObject button;
    public TextAsset file;
    
    public static bool isNPC1Talked;


    private void Start()
    {
        DialogueManager.instance.GetTextFiel(file);
        DialogueManager.instance.GetTextFromFile(file);       
    }

    
    private void Update()
    {
        if (button.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            if (gameObject.name == "NPC1")
            {
                isNPC1Talked = true;  
            }
            DialogueManager.instance.talkPanel.SetActive(true);
            button.SetActive(false);

            
        }
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        button.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        button.SetActive(false);
    }

}
