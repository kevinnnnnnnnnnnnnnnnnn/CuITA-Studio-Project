using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenManager : MonoBehaviour,ISaveable
{
    public static DoorOpenManager instance;

    public Dictionary<string, bool> isOpenDoorDict = new Dictionary<string, bool>();
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }
    }


    private void Start()
    {
        ISaveable saveable = this;
        saveable.SaveableRegister();
    }


    public GameSaveData GenerateSaveData()
    {
        GameSaveData saveData = new GameSaveData();

        saveData.isDoorOpenDict = this.isOpenDoorDict;
        
        return saveData;
    }

    public void ReStoreGameDate(GameSaveData saveData)
    {
        this.isOpenDoorDict = saveData.isDoorOpenDict;
    }
}