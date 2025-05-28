using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;


public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager instance;

    //储存json的文件夹路径
    private string jsonFolder;

    //储存Isaveavle的列表，保存含Isaveable接口的数据对象
    public List<ISaveable> saveableList = new List<ISaveable>();

    //key为含接口的脚本名字，value为GameSaveData对象，即保存的数据
    public Dictionary<string, GameSaveData> saveDataDict = new Dictionary<string, GameSaveData>();
    

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
        
        jsonFolder = Application.persistentDataPath + "/SAVE/";
    }


    private void OnEnable()
    {
        EventHandler.StartGameEvent += OnStartGameEvent;
    }

    private void OnDisable()
    {
        EventHandler.StartGameEvent -= OnStartGameEvent;
    }

    
    /// <summary>
    /// 开始新游戏时注册的事件
    /// </summary>
    /// <param name="obj"></param>
    public void OnStartGameEvent(int obj)
    {
        //最终的文件路径
        var resultPath = jsonFolder + "date.sav";

        //如果已经存在文件，则删除数据
        if (File.Exists(resultPath))
        {
            File.Delete(resultPath);
        }
    }
    

    public void Register(ISaveable saveable)
    {
        saveableList.Add(saveable);
    }


    /// <summary>
    /// 保存数据
    /// </summary>
    public void Save()
    {
        //保存新数据前，清空旧数据
        saveDataDict.Clear();
    
        //遍历需要保存的数据列表，添加到dictionary中，通过GenerateSaveData返回GameSaveData对象
        foreach (var saveable in saveableList)
        {
            saveDataDict.Add(saveable.GetType().Name, saveable.GenerateSaveData());
        }
        
        var resultPath = jsonFolder + "data.sav";
        
        //将dictionary序列化为json字符串
        var jsonDate = JsonConvert.SerializeObject(saveDataDict, Formatting.Indented);
    
        if (!File.Exists(resultPath))
        {
            //判断如果当前没有储存文件夹，就创建此文件夹
            Directory.CreateDirectory(jsonFolder);
        }
        
        //将json字符串写入文件
        File.WriteAllText(resultPath, jsonDate);
    }


    

    public void Load()
    {
        var resultPath = jsonFolder + "data.sav";
    
        //如果文件夹不存在，则取消加载
        if (!File.Exists(resultPath))
            return;
        
        //读取json字符串
        var stringData = File.ReadAllText(resultPath);
    
        //将读取的字符串反序列化为dictionary,string为脚本名，GameSaveData为保存的数据对象
        var jsonDataDict = JsonConvert.DeserializeObject<Dictionary<string, GameSaveData>>(stringData);
    
        foreach (var saveable in saveableList)
        {
            saveable.ReStoreGameDate(jsonDataDict[saveable.GetType().Name]);
        }
    }
}
