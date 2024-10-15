using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

/// <summary>
/// 需要保存的游戏数据
/// </summary>
public class GameSaveData
{

    public Dictionary<string, bool> isLightTokenDict;
    
    public Dictionary<string,bool> isDoorOpenDict;

    public string currentSceneName;
    public float playerPosX;
    public float playerPosY;

}