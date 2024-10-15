using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// 保存所需的接口
/// </summary>
public interface ISaveable
{
    void SaveableRegister()
    {
        SaveLoadManager.instance.Register(this);
    }
    
    /// <summary>
    /// 生成保存数据，返回GameSaveData类型
    /// </summary>
    /// <returns></returns>
    GameSaveData GenerateSaveData();
    
    
    
    /// <summary>
    /// 恢复数据
    /// </summary>
    /// <param name="saveData"></param>
    void ReStoreGameDate(GameSaveData saveData);
}
