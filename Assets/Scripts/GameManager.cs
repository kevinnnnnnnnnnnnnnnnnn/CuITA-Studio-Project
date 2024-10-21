using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CulTA
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        

        public bool isGameStart;

        public float currentScenePlayerPosX;
        public float currentScenePlayerPosY;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
    }
}
