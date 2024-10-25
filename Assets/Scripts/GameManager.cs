using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace CulTA
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        

        public bool isGameStart;

        public float currentScenePlayerPosX;
        public float currentScenePlayerPosY;


        public TMP_InputField inputPlayerName;
        public string playerName;

        
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }


        private void Start()
        {
            inputPlayerName.onValueChanged.AddListener(OnPlayerNameChanged);
        }

        private void OnPlayerNameChanged(string value)
        {
            playerName = value;
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log(playerName);
            }
        }
    }
}
