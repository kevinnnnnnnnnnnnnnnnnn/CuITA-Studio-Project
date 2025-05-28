using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;
using Task = System.Threading.Tasks.Task;

public class InitGroundManager : MonoBehaviour
{
    public static InitGroundManager instance;

    public GameObject groundPrefab;
    public GameObject groundPrefabNew;
    public GameObject finishGroundPrefab;
    public GameObject finishGroundPrefabNew;
    
    public GameObject transitionSameGroundPrefab;
    public GameObject transitionOtherGroundPrefab;
    public GameObject getSkillGroundPrefab;
    
    public GameObject player;

    public int groundCount;//台阶数量
    public int groundCountNew;//第二处平台的台阶数量
    public string testScene;

    public List<GameObject> groundList = new List<GameObject>();
    
    private readonly Vector2 _step = new Vector2(4.25f, 7.5f);
    
    
    
    
    
    private void Awake()
    {
        if(instance != null)
            Destroy(instance);
        instance = this;
    }

    

    void Start()
    {
        InitGround(groundCount, new Vector2(0, 0), GroundType.TransitionSameScene);

#if false //测试场景切换
        OtherSceneTransitionManager.instance.TransitionOtherScene("Origin", testScene, 
            new Vector2(141, 20));
#endif
    }




    /// <summary>
    /// 自动化生成地图
    /// </summary>
    /// <param name="count">生成数量</param>
    /// <param name="spawnPos">开始生成位置</param>
    /// <param name="groundType">终点方块类型</param>
    public async void InitGround(int count, Vector2 spawnPos, GroundType groundType)
    {
        if (groundList != null)
        {
            foreach (var ground in groundList)
            {
                Destroy(ground);
            }
            groundList.Clear();
        }
        
        //Debug.Log("initGround");
        //Vector2 spawnPos = new Vector2(0, 0);

        int randomDir = -1;
        for (int i = 0; i < count; i++)
        {
            //Debug.Log("initGround " + i);
            
            //int randomDir = Random.Range(0f, 1f) > 0.5 ? 1 : -1;
            if (i % 5 == 0)
                randomDir = -randomDir;

            GameObject ground;

            if (i != count - 1)
            {
                ground = Instantiate(groundPrefab, spawnPos - Vector2.up * 2, Quaternion.identity);
            }
            else if(groundType == GroundType.TransitionSameScene)
            {
                ground = Instantiate(transitionSameGroundPrefab, spawnPos - Vector2.up * 2, Quaternion.identity);
            }
            else if(groundType == GroundType.TransitionOtherScene)
            {
                ground = Instantiate(transitionOtherGroundPrefab, spawnPos - Vector2.up * 2, Quaternion.identity);
            }
            else if(groundType == GroundType.GetSkill)
            {
                ground = Instantiate(getSkillGroundPrefab, spawnPos - Vector2.up * 2, Quaternion.identity);
            }
            else
            {
                ground = Instantiate(finishGroundPrefab, spawnPos - Vector2.up * 2, Quaternion.identity);
            }
            


            ground.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
            ground.GetComponent<SpriteRenderer>().DOFade(1, 2f);

            //Task延迟实现方块的逐个变化，位置以及透明度
            await Task.Delay(250);

            groundList?.Add(ground);

            ground.transform.parent = transform;
            ground.transform.DOMove(ground.transform.position + Vector3.up * 2, 0.15f); //.SetDelay(0.15f * i);

            spawnPos += new Vector2(_step.x * randomDir, _step.y); //变换生成方块的位置
        }
    }
    
    
    
}
