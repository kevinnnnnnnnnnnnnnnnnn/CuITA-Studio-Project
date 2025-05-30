using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CulTA
{
    public class TimeLineManager : MonoBehaviour
    {
        public static TimeLineManager instance;
        
        [Header("场景TimeLine")] 
        public PlayableDirector menuDirector;
        public PlayableDirector gameSceneDirector;
        public PlayableDirector gameScene1Director;
        public PlayableDirector gameScene2Director;
        public PlayableDirector gameScene3Director;
        public PlayableDirector gameScene4Director;

        [Header("TimeLine是否已启动")] 
        public bool menuTimeLineStart;
        public bool gameSceneTimeLineStart;
        public bool gameScene1TimeLineStart;
        public bool gameScene2TimeLineStart;
        public bool gameScene3TimeLineStart;
        public bool gameScene4TimeLineStart;
        
        public Button menuButton;
        

        public GameObject inputPanel;


        private void Awake()
        {
            instance = this;
        }
        
        

        private void OnEnable()
        {
            menuDirector.stopped += OnSetPlayerMove;
            menuDirector.stopped += OnActiveInputPanel;
            
            gameSceneDirector.stopped += OnSetPlayerMove;
            gameScene1Director.stopped += OnSetPlayerMove;
            gameScene2Director.stopped += OnSetPlayerMove;
            gameScene3Director.stopped += OnSetPlayerMove;
            gameScene4Director.stopped += OnSetPlayerMove;
        }

        
        private void OnDisable()
        {
            menuDirector.stopped -= OnSetPlayerMove;
            menuDirector.stopped -= OnActiveInputPanel;
            
            gameSceneDirector.stopped -= OnSetPlayerMove;
            gameScene1Director.stopped -= OnSetPlayerMove;
            gameScene2Director.stopped -= OnSetPlayerMove;
            gameScene3Director.stopped -= OnSetPlayerMove;
            gameScene4Director.stopped -= OnSetPlayerMove;
        }


        private void Update()
        {
            SetTimeLine();
        }


        public void SetTimeLine()
        {
            var currentSceneName = SceneManager.GetActiveScene().name;

            if (currentSceneName == "Menu" && !menuTimeLineStart)
            {
                menuDirector.Play();
                menuTimeLineStart = true;
                TransitionManager.instance.player.GetComponent<PlayerMove>().enabled = false;
                menuButton.interactable = false;
            }
            if (currentSceneName == "GameScene" && !gameSceneTimeLineStart && BackStoryManager.instance.isGameScene)
            {
                gameSceneDirector.Play();
                gameSceneTimeLineStart = true;
                TransitionManager.instance.player.GetComponent<PlayerMove>().enabled = false;
                menuButton.interactable = false;
            }
            if (currentSceneName == "GameScene1" && !gameScene1TimeLineStart)
            {
                gameScene1Director.Play();
                gameScene1TimeLineStart = true;
                TransitionManager.instance.player.GetComponent<PlayerMove>().enabled = false;
                menuButton.interactable = false;
            }
            if (currentSceneName == "GameScene2" && !gameScene2TimeLineStart)
            {
                gameScene2Director.Play();
                gameScene2TimeLineStart = true;
                TransitionManager.instance.player.GetComponent<PlayerMove>().enabled = false;
                menuButton.interactable = false;
            }
            if (currentSceneName == "GameScene3" && !gameScene3TimeLineStart)
            {
                gameScene3Director.Play();
                gameScene3TimeLineStart = true;
                TransitionManager.instance.player.GetComponent<PlayerMove>().enabled = false;
                menuButton.interactable = false;
            }
            if (currentSceneName == "GameScene4" && !gameScene4TimeLineStart)
            {
                gameScene4Director.Play();
                gameScene4TimeLineStart = true;
                TransitionManager.instance.player.GetComponent<PlayerMove>().enabled = false;
                menuButton.interactable = false;
            }
        }



        public void OnSetPlayerMove(PlayableDirector director)
        {
            TransitionManager.instance.player.GetComponent<PlayerMove>().enabled = true;
            menuButton.interactable = true;
        }

        public void OnActiveInputPanel(PlayableDirector director)
        {
            //inputPanel.SetActive(true);
        }
    }
}
