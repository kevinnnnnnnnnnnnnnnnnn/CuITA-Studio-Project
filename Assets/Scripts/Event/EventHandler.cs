using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class EventHandler
{
    /// <summary>
    /// 场景转换后事件
    /// </summary>
    public static event Action<float, float> AfterTransition;
    public static void CallAfterTransition(float targetX, float targetY)
    {
        AfterTransition?.Invoke(targetX, targetY);
    }
    
    /// <summary>
    /// 场景转换前事件
    /// </summary>
    public static event Action BeforeTransition;
    public static void CallBeforeTransition()
    {
        BeforeTransition?.Invoke();
    }


    /// <summary>
    /// 开始游戏时初始化数据的事件
    /// </summary>
    public static event Action<int> StartGameEvent;
    public static void CallStartGameEvent(int obj)
    {
        StartGameEvent?.Invoke(obj);
    }


    /// <summary>
    /// 在菜单开始新游戏的事件
    /// </summary>
    public static event Action<string> MenuStartNewGameEvent;
    public static void CallMenuStartNewGameEvent(string toScene)
    {
        MenuStartNewGameEvent?.Invoke(toScene);
    }
    

    /// <summary>
    /// 在菜单继续游戏的事件
    /// </summary>
    public static event Action<string, float, float> MenuContinueGameEvent;

    public static void CallMenuContinueGameEvent(string toScene, float targetX, float targetY)
    {
        MenuContinueGameEvent?.Invoke(toScene, targetX, targetY);
    }
}
