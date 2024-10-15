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
        button.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        button.SetActive(false);
    }

    private void Update()
    {
        if (button.activeSelf && Input.GetKeyDown(KeyCode.R))
        {
            talkUI.SetActive(true);
        }
    }

}