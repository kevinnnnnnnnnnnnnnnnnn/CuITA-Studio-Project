using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using DG;

public class SameSceneTransitionManager : MonoBehaviour
{
    public static SameSceneTransitionManager instance; 
        
    public Vector2 playerTargetPosition;

    public GameObject player;
    

    //场景转换特效变量
    public bool isFade;
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration;
    public int loadSceneDuration;

     public Slider loadSlider;
    // public GameObject loadGear;

    
    public bool transitionFinished;
    
    private void Awake()
    {
        if(instance != null)
            Destroy(instance);
        instance = this;
    }
    
    

    /// <summary>
    /// 相同场景内的传送
    /// </summary>
    /// <param name="groundCreateCount">生成方块数量</param>
    /// <param name="spawnPosition">开始生成位置</param>
    public async void TransitionSameScene(int groundCreateCount, Vector2 spawnPosition)
    {
        transitionFinished = false;
        
        StartCoroutine(Fade(1));
        await Task.Delay((int)fadeDuration * 1000); 
        
        //加载页面进度条
        LoadSceneAnim();
        
        await Task.Delay(loadSceneDuration * 1100);
        
        //将人物移动至正确场景位置
        player.transform.position = new Vector2(spawnPosition.x, spawnPosition.y + 6); 

        //将新地图的groundPrefab改为新的方块
        InitGroundManager.instance.groundPrefab = InitGroundManager.instance.groundPrefabNew;
        InitGroundManager.instance.finishGroundPrefab = InitGroundManager.instance.finishGroundPrefabNew;
        
        StartCoroutine(Fade(0));

        transitionFinished = true;
        
        InitGroundManager.instance.InitGround(groundCreateCount, spawnPosition,GroundType.GetSkill);
        InitGroundManager.instance.InitGround(groundCreateCount + 5,
            spawnPosition + new Vector2(0, 7.5f * (groundCreateCount + 3)),
            GroundType.TransitionOtherScene);
    }


    
    /// <summary>
    /// 场景渐入渐出
    /// </summary>
    /// <param name="targetAlpha">Alpha目标值</param>
    /// <returns></returns>
    private IEnumerator Fade(float targetAlpha)
    {
        isFade = true;

        fadeCanvasGroup.blocksRaycasts = true;

        float speed = Mathf.Abs((fadeCanvasGroup.alpha - targetAlpha) / fadeDuration);

        while (!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha))
        {
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
            yield return null;
        }

        fadeCanvasGroup.blocksRaycasts = false;

        isFade = false;

        if (targetAlpha == 0)
        {
            loadSlider.value = 0;
            //loadGear.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }


    /// <summary>
    /// 加载场景齿轮动画以及加载进度条
    /// </summary>
    public void LoadSceneAnim()
    {
        //齿轮循环旋转
        // loadGear.transform.DORotate(new Vector3(0, 0, -360), 1.5f, RotateMode.FastBeyond360)
        //     .SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
        
        //进度条加载
        loadSlider.DOValue(1, loadSceneDuration).SetEase(Ease.Linear);
    }
}
