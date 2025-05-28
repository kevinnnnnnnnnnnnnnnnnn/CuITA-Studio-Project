using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
using Task = System.Threading.Tasks.Task;

public class TipsTextUI : MonoBehaviour
{
    public TextMeshProUGUI tipsText;

    public List<string> tipsList = new List<string>();

    private void Start()
    {
        GetRandomTip();
    }

    
    /// <summary>
    /// 随机获取Tips并显示Text
    /// </summary>
    public async void GetRandomTip()
    {
        while (true)
        {
            int randomIndex = Random.Range(0, tipsList.Count);
            tipsText.text = tipsList[randomIndex];
            
            await Task.Delay(5000);
            //Debug.Log("New Tip");
        }
    }
    
    
}

