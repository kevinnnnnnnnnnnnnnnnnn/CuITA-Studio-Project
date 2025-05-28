using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectLightManager : MonoBehaviour,ISaveable
{
    public static CollectLightManager instance;
    
    //在CollectLight中awake加入列表
    public List<CollectLight> collectLightList = new List<CollectLight>();
    
    //在CollectLight中，加入字典，记录每一个光球的状态，是否已经被拾取
    public Dictionary<string,bool> isLightTokenDict = new Dictionary<string, bool>();
    
    //在TransitionManager中，重置max和current的数量
    public int maxCollectLightNum;//需要手机的光球数量
    public int currentCollectLightNum;//当前已经收集的光球数量
    

    

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    private void Start()
    {
        ISaveable saveable = this;
        saveable.SaveableRegister();
    }

    /// <summary>
    /// 在CollectLight中更新当前已经收集的光球数量，根据目前已收集的光球数量来提高Player亮度
    /// </summary>
    public void UpdateCurrentCollectLightNum()
    {
        currentCollectLightNum++;
        PlayerLightManager.instance.SetPlayerLight();
    }
    
    
    /// <summary>
    /// 在CollectLight中更新收集光球的最大数量
    /// </summary>
    public void UpdateMaxCollectLightNum()
    {
        maxCollectLightNum++;
    }
    


    // /// <summary>
    // /// 将gameSaveData的数据恢复到游戏中
    // /// </summary>
    // /// <param name="saveData">GameSaveData的数据</param>
    // public void ReStoreGameDate(GameSaveData saveData)
    // {
    //     collectLightDic = saveData.isLightTokenDic;
    // }
    public GameSaveData GenerateSaveData()
    {
        GameSaveData saveData = new GameSaveData();
        
        saveData.isLightTokenDict = this.isLightTokenDict;
        // saveData.maxCollectLightNum = this.maxCollectLightNum;
        // saveData.currentCollectLightNum = this.currentCollectLightNum;

        return saveData;
    }

    public void ReStoreGameDate(GameSaveData saveData)
    {
        this.isLightTokenDict = saveData.isLightTokenDict;
        // this.maxCollectLightNum = saveData.maxCollectLightNum;
        // this.currentCollectLightNum = saveData.currentCollectLightNum;



        foreach (var light in collectLightList)
        {
            if (isLightTokenDict.ContainsKey(light.name) && !isLightTokenDict[light.name])
            {
                light.gameObject.SetActive(true);
            }
            else if(isLightTokenDict.ContainsKey(light.name) && isLightTokenDict[light.name])
            {
                light.gameObject.SetActive(false); 
                
                UpdateCurrentCollectLightNum();
            }
        }
    }
}
