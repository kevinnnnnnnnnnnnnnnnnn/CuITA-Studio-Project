using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CulTA
{
    public class PlayerDead : MonoBehaviour
    {
        public GameObject player;
        public GameObject deadPanel;

        public PlayerMove playerMove;


        private void Awake()
        {
            playerMove = GetComponent<PlayerMove>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("DeadArea"))
            {
                StartCoroutine(PlayerDeadWait());
                
                playerMove.enabled = false;
                player.GetComponent<SpriteRenderer>().enabled = false;
                player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }



        IEnumerator PlayerDeadWait()
        {
            yield return new WaitForSeconds(2f);
            
            deadPanel.SetActive(true);
        }
    }
}
