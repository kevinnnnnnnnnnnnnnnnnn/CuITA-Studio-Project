using System;
using System.Collections;
using System.Collections.Generic;
using CulTA;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    //TODO：挂在Menu的Canvas上，将Canvas挂在Button事件中，实现Menu进入游戏，退出游戏等功能

    private string toScene;
    private float targetX;
    private float targetY;

    public Button continueButton;

    private void Update()
    {
        UpdatePlayerPos();

        if (continueButton != null)
        {
           if (!GameManager.instance.isGameStart)
           {
               continueButton.interactable = false;
           }
           else
           {
               continueButton.interactable = true;
           }         
        }

    }

    public void StartNewGame(string to)
    {
        Debug.Log("Start New Game");
        EventHandler.CallMenuStartNewGameEvent(to);
        
        GameManager.instance.isGameStart = true;
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
        
        Debug.Log("Back to Menu");
    }


    public void UpdatePlayerPos()
    {
        toScene = TransitionManager.instance.currentScene;
        targetX = TransitionManager.instance.currentPosX;
        targetY = TransitionManager.instance.currentPosY;
    }

}
