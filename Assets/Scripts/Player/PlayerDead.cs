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
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("DeadArea"))
            {
                StartCoroutine(PlayerDeadWait());
            }
        }



        IEnumerator PlayerDeadWait()
        {
            yield return new WaitForSeconds(2f);
            
            deadPanel.SetActive(true);
                       
            player.SetActive(false);
        }
    }
}
