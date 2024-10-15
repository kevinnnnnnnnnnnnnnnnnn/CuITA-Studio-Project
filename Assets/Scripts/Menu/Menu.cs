using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    //TODO：挂在Menu的Canvas上，将Canvas挂在Button事件中，实现Menu进入游戏，退出游戏等功能

    private string toScene;
    private float targetX;
    private float targetY;

    private void Update()
    {
        UpdatePlayerPos();
    }

    public void StartNewGame(string to)
    {
        EventHandler.CallMenuStartNewGameEvent(to);
    }

    public void ContinueGame()
    {
        EventHandler.CallMenuContinueGameEvent(toScene, targetX, targetY);
    }

    public void ExitGame()
    {
        Application.Quit();
    }


    public void BackToMenu()
    {
        SaveLoadManager.instance.Save();
        
        string currentScene = SceneManager.GetActiveScene().name;

        TransitionManager.instance.canTransition = true;
        TransitionManager.instance.Transition(currentScene, "Menu", -5, 1.5f);
        TransitionManager.instance.canTransition = false;
    }


    public void UpdatePlayerPos()
    {
        toScene = TransitionManager.instance.currentScene;
        targetX = TransitionManager.instance.currentPosX;
        targetY = TransitionManager.instance.currentPosY;
    }
}
