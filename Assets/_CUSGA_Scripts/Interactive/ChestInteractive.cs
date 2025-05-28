using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteractive : MonoBehaviour
{
    public GameObject button;
    public Sprite chestOpen;
    public bool isOpenedChest;

    


    private void Update()
    {
        OpenChest();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isOpenedChest)
            return;
        
        button.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (isOpenedChest)
            return;
            
        button.SetActive(false);
    }

    public void OpenChest()
    {
        if (button.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            isOpenedChest = true;
            GetComponent<SpriteRenderer>().sprite = chestOpen;
            button.SetActive(false);
        }
    }
}
