using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class BackMenuUI : MonoBehaviour
{
    [SerializeField]private bool _canBackMenu;

    [SerializeField] private GameObject backMenu;


    private void Update()
    {
        ControlCanBackMenu();

        if (_canBackMenu)
            backMenu.SetActive(true);
        else 
            backMenu.SetActive(false);
    }


    public void ControlCanBackMenu()
    {
        if (SceneManager.GetActiveScene().name == "Persistent" || SceneManager.GetActiveScene().name == "Menu")
        {
            _canBackMenu = false;
        }
        else
        {
            _canBackMenu = true;
        }
    }
}
