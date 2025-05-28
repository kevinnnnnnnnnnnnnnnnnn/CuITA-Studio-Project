using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using Task = System.Threading.Tasks.Task;

public class SetGround : MonoBehaviour
{
    public GameObject setGroundPrefab;

    public int setMaxCount;//允许放置的最大方块数量
    public int setCurCount;//当前放置的方块数量
    
    public bool skillOpen;//技能是否为开启状态
    public bool isSetGround;//是否要放置方块
    public bool isSetting;//是否正在放置方块
    
    private int _spawnDir;//生成ground的方向

    public GameObject player;
    public TextMeshProUGUI skillText;

    private Vector2 step = new Vector2(4.25f, 7.5f);
    
    public List<GameObject> groundList = new List<GameObject>();


    private void Start()
    {
        skillText.text = "当前可放置" + setMaxCount + "个方块";
    }


    void Update()
    {
        OpenSkill();
        SetSkill();
        Skill();
        CollectGround();
    }


    
    /// <summary>
    /// 检测输入，开启技能
    /// </summary>
    public void OpenSkill()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            skillOpen = !skillOpen;
            
            skillText.text = "当前可放置" + setMaxCount + "个方块";

            isSetGround = false;
            isSetting = false;
        }
    }

    
    
    /// <summary>
    /// 检测输入，判断是否要放置方块
    /// </summary>
    public void SetSkill()
    {
        if (!skillOpen)
            return;
        if (isSetting)
            return;
        
        
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            isSetGround = true;
            _spawnDir = 1;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            isSetGround = true;
            _spawnDir = -1;
        }
        
    }


    
    /// <summary>
    /// 放置方块
    /// </summary>
    public void Skill()
    {
        if (!skillOpen)
            return;
        if (!isSetGround)
            return;
        if (isSetting)
            return;

        if (setCurCount >= setMaxCount)
            return;
        
        
        GameObject ground;


        ground = Instantiate(setGroundPrefab,
            new Vector2(player.transform.position.x + step.x * _spawnDir, player.transform.position.y + step.y - 6 - 2),
            Quaternion.identity);

        //生成ground动画
        ground.transform.parent = InitGroundManager.instance.transform;
        ground.transform.DOMove(ground.transform.position + Vector3.up * 2.195f, 0.4f)
            .OnPlay(() => isSetting = true)
            .OnComplete(() => isSetting = false);
        
        groundList.Add(ground);
        setCurCount++;//当前放置的数量 +1

        skillText.text = "当前可放置" + (setMaxCount - setCurCount) + "个方块";
        
        
        isSetGround = false;
    }



    /// <summary>
    /// 关闭技能时，回收方块点数
    /// </summary>
    public void CollectGround()
    {
        if (skillOpen)
            return;
        
        foreach (var ground in groundList)
        {
            Destroy(ground);
        }

        groundList.Clear();
        
        setCurCount = 0;
    }
}
