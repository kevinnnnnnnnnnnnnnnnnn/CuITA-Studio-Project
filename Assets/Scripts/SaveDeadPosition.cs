using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CulTA
{
    public class SaveDeadPosition : MonoBehaviour
    {
        public bool canSave;
        public GameObject saveButton;


        private void Update()
        {
            SavePosition();
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                saveButton.SetActive(true);
                canSave = true;       
            }
   
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                saveButton.SetActive(false);
                canSave = false;                
            }

        }

        public void SavePosition()
        {
            if (canSave && Input.GetKeyDown(KeyCode.E))
            {
                GameManager.instance.currentScenePlayerPosX = TransitionManager.instance.player.transform.position.x;
                GameManager.instance.currentScenePlayerPosY = TransitionManager.instance.player.transform.position.y;
            }

        }
    }
}
