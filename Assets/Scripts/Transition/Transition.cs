using System;
using UnityEngine;

public class Transition : MonoBehaviour
{
    public string sceneFrom;
    public string sceneTo;

    public float targetX;
    public float targetY;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            TransitionManager.instance.Transition(sceneFrom, sceneTo, targetX, targetY);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TransitionManager.instance.CanTransition(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TransitionManager.instance.CanTransition(false);
        }
    }
}

