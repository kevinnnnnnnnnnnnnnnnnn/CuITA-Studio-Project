using System;
using System.Collections;
using CulTA;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Menu : MonoBehaviour
{
    private string toScene;
    private float targetX;
    private float targetY;

    public Button continueButton;

    public Button exitButton;



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
        StartCoroutine(StarNewGame(to));
    }

    public void ContinueGame()
    {
        EventHandler.CallMenuContinueGameEvent(toScene, targetX, targetY);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
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


    IEnumerator StarNewGame(string to)
    {
        MenuGlobalLight.instance.SetMenuLightIntensity();
        
        if(exitButton != null)
            exitButton.interactable = false;
        
        yield return new WaitForSeconds(2f);
        
        if(exitButton != null)
            exitButton.interactable = true;
        
        Debug.Log("Start New Game");
        EventHandler.CallMenuStartNewGameEvent(to);

        GameManager.instance.isGameStart = true;
    }
}
