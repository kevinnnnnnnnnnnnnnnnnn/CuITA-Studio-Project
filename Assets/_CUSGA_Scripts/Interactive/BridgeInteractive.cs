using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;


public class BridgeInteractive : MonoBehaviour
{
    public GameObject button;
    
    public GameObject bridge;
    public bool isBridgeMoved;

    public Transform targetPos;

    public float moveTime;


    private void Update()
    {
        BridgeControllor();
    }


    /// <summary>
    /// 平台下降
    /// </summary>
    public void MoveDownBridge()
    {
        bridge.transform.DOMove(targetPos.position, moveTime).OnComplete(() => isBridgeMoved = true);
    }

    
    /// <summary>
    /// 平台上升
    /// </summary>
    public void MoveUpBridge()
    {
        bridge.transform.DOMove(new Vector2(0, 0), moveTime).OnComplete(() => isBridgeMoved = false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && TalkButtonCUSGA.isNPC1Talked)
        {
            button.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && TalkButtonCUSGA.isNPC1Talked)
        {
            button.SetActive(false);
        }
    }

    // private void OnTriggerStay2D(Collider2D other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         GameManager.instance.playerNew.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -1000), ForceMode2D.Impulse);
    //     }
    // }


    public void BridgeControllor()
    {
        if (!isBridgeMoved && button.activeSelf && Input.GetKeyDown(KeyCode.E) && TalkButtonCUSGA.isNPC1Talked)
        {
            MoveDownBridge();
            button.SetActive(false);
        }
        else if (isBridgeMoved && button.activeSelf && Input.GetKeyDown(KeyCode.E) && TalkButtonCUSGA.isNPC1Talked)
        {
            MoveUpBridge();
            button.SetActive(false);
        }
    }
}
