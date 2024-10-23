using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CulTA
{
    public class TransitionDoor : MonoBehaviour
    {
        public GameObject targetDoorPos;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                if (TransitionManager.instance.player.GetComponent<Rigidbody2D>().velocity.x >= 0)
                    TransitionManager.instance.player.transform.position =
                        targetDoorPos.transform.position + Vector3.right * 1.5f;
                if (TransitionManager.instance.player.GetComponent<Rigidbody2D>().velocity.x < 0)
                    TransitionManager.instance.player.transform.position =
                        targetDoorPos.transform.position + Vector3.left * 1.5f;
            }
        }
    }
}
