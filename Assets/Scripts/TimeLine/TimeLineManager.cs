using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

namespace CulTA
{
    public class TimeLineManager : MonoBehaviour
    {
        [Header("场景TimeLine")]
        public PlayableDirector menuDirector;
        public PlayableDirector gameSceneDirector;
        public PlayableDirector gameScene1Director;
        public PlayableDirector gameScene2Director;
        public PlayableDirector gameScene3Director;
        
        [Header("TimeLine是否已启动")]
        public bool menuTimeLineStart;
        public bool gameSceneTimeLineStart;
        public bool gameScene1TimeLineStart;
        public bool gameScene2TimeLineStart;
        public bool gameScene3TimeLineStart;


        private void Update()
        {
            var currentSceneName = SceneManager.GetActiveScene().name;

            if (currentSceneName == "Menu" && !menuTimeLineStart)
            {
                menuDirector.Play();
                menuTimeLineStart = true;
            }
            else if (currentSceneName == "GameScene" && !gameSceneTimeLineStart)
            {
                gameSceneDirector.Play();
                gameSceneTimeLineStart = true;
            }
            else if (currentSceneName == "GameScene1" && !gameScene1TimeLineStart)
            {
                gameScene1Director.Play();
                gameScene1TimeLineStart = true;
            }
            else if (currentSceneName == "GameScene2" && !gameScene2TimeLineStart)
            {
                gameScene2Director.Play();
                gameScene2TimeLineStart = true;
            }
            else if (currentSceneName == "GameScene3" && !gameScene3TimeLineStart)
            {
                gameScene3Director.Play();
                gameScene3TimeLineStart = true;
            }
        }
    }
}
