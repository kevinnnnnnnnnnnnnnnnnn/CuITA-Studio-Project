using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace CulTA
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        public GameObject player;
        public Canvas joystickCanvas;
        public string changePlayerSceneName;


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
            player.SetActive(false);
        }

        private void Update()
        {
            ChangePlayer();
            ChangeControllMode();
        }

        private void OnPlayerNameChanged(string value)
        {
            playerName = value;
        }

        public void ChangePlayer()
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            if(currentSceneName == changePlayerSceneName)
                player.SetActive(true);
        }

        public void ChangeControllMode()
        {
            if (Input.touchCount == 0)
            {
                joystickCanvas.enabled = false;
            }

            if (Input.touchCount != 0)
            {
                joystickCanvas.enabled = true;
            }
        }
    }
}
