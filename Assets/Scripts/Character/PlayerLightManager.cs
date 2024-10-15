using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

/// <summary>
/// 控制玩家子物体光源的亮度
/// </summary>
public class PlayerLightManager : MonoBehaviour
{
    public static PlayerLightManager instance;
    
    [SerializeField] private float playerLightStrength;//玩家总共增加的光照强度
    [SerializeField] public GameObject playerLight;//玩家的子物体光源
    [SerializeField] public float playerFirstLight;//玩家最初的光源亮度


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        playerLight.GetComponent<Light2D>().intensity = playerFirstLight;
    }


    /// <summary>
    /// 计算每次拾取光球后增加的亮度，并增加玩家光照，通过单例在CollectLightManager中调用
    /// </summary>
    public void SetPlayerLight()
    {
        var playerLightIncrease = (1.0f / CollectLightManager.instance.maxCollectLightNum) * playerLightStrength;
        
        playerLight.GetComponent<Light2D>().intensity += playerLightIncrease;
    }
    
    
}
