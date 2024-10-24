using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BackStoryManager : MonoBehaviour
{
    public static BackStoryManager instance;
    
    public bool isGameScene;


    public GameObject gameSceneStory;



    private void Awake()
    {
        instance = this;
    }
    

    private void FixedUpdate()
    {
        StartStory();
    }

    public void StartStory()
    {
        var currentSceneName = SceneManager.GetActiveScene().name;
        
        if (currentSceneName == "GameScene" && !isGameScene)
        {
            gameSceneStory.SetActive(true);
            if(BackGroundStory.instance.isStoryOver)
                isGameScene = true;
        }
    }
}

