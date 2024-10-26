using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CulTA
{
    public class Teach : MonoBehaviour
    {
        public GameObject teachPanel;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag("Player"))
                teachPanel.SetActive(true);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
                teachPanel.SetActive(false);
        }
    }
}
