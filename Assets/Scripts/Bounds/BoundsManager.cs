using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using Task = System.Threading.Tasks.Task;

public class BoundsManager : MonoBehaviour 
{
    public static BoundsManager instance;
    
    public PolygonCollider2D bounds;
    public GameObject cinemachineVirtualCamera;


    private void Awake()
    {
        if (bounds != null)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }
        
        bounds = GetComponent<PolygonCollider2D>();
    }



    public void UpdateBounds(PolygonCollider2D newBounds)
    {
        bounds.points = newBounds.points;
        
        UpdateCinemachineConfiner();
    }

    public async void UpdateCinemachineConfiner()
    {
        var cinemaConfiner = cinemachineVirtualCamera.GetComponent<CinemachineConfiner2D>();

        cinemaConfiner.m_BoundingShape2D = null;

        await Task.Delay(10);
        
        cinemaConfiner.m_BoundingShape2D = bounds;
    }
}
