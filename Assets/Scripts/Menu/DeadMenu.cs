using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CulTA
{
    public class DeadMenu : MonoBehaviour
    {
        public GameObject deadPanel;
        public GameObject player;

        public void ReStoreGame()
        {
            SaveLoadManager.instance.Load();
            
            deadPanel.SetActive(false);


            player.transform.position = new Vector2(GameManager.instance.currentScenePlayerPosX,
                                                            GameManager.instance.currentScenePlayerPosY);
            player.SetActive(true);
        }
    }
}
