using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TalkButton : MonoBehaviour
{
    public GameObject button;
    public GameObject talkUI;

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
        if (button.activeSelf && (Input.GetKeyDown(KeyCode.E) || AndroidInputButton.instance.isClickedInteractiveButton))
        {
            talkUI.SetActive(true);
        }
    }
}