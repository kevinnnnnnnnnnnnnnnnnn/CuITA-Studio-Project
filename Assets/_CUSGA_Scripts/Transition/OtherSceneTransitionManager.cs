using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Random = UnityEngine.Random;


public class OtherSceneTransitionManager : MonoBehaviour
{
    public static OtherSceneTransitionManager instance;
    
    [Header("组件获取")]
    public CanvasGroup fadeCanvasGroup;
    public Slider loadSlider;
    public CinemachineVirtualCamera virtualCamera;

    [Header("切换场景的角色变化")]
    public GameObject player;
    public GameObject playerNew;
 
    [Header("加载时间")]
    public float fadeDuration = 1f;
    public int loadSceneDuration; 
    public bool isFade;

    [Header("随机变化目标花朵")]
    public GameObject targetFlower;
    public List<Sprite> targetFlowerSprite;

    [Header("技能开关")] 
    public GameObject setGroundSkill;
    
    
    
    
    private void Awake()
    {
        if(instance != null)
            Destroy(instance);
        instance = this;
    }

    
    
    
    /// <summary>
    /// 传送至其他场景
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="targetPosition"></param>
    public async void TransitionOtherScene(string from, string to, Vector2 targetPosition)
    {
        //摄像机旋转
        // StartCoroutine(RotateCamera());
        // await Task.Delay(1000);

        int index = Random.Range(0, targetFlowerSprite.Count);
        targetFlower.GetComponent<Image>().sprite = targetFlowerSprite[index];
        
        
        StartCoroutine(Fade(1));
        
        await Task.Delay((int)fadeDuration * 1000); 
        
        //加载页面的齿轮以及进度条
        LoadSceneAnim();
        
        await Task.Delay(loadSceneDuration * 1100);
        
        //保留初始场景
        if(from != "Origin")
            SceneManager.UnloadSceneAsync(from);
        if (to == "Menu")
            SceneManager.UnloadSceneAsync("Origin");
        
        SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive);
        
        Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);

        await Task.Delay(1000);
        
        SceneManager.SetActiveScene(newScene);

        
        //切换场景后，新旧角色显示切换，设置playerNew位置，将摄像机调整到新角色位置

        
        //从Origin传出时才执行以下操作
        if (from == "Origin") 
        {
            player.SetActive(false);
            playerNew.SetActive(true);
            
            playerNew.transform.position = targetPosition;
            virtualCamera.Follow = playerNew.transform;
            virtualCamera.m_Lens.OrthographicSize = 30;
            
            //切换场景后，旧场景的元素隐藏
            foreach (var ground in InitGroundManager.instance.groundList)
            {
                ground.SetActive(false);
            }
            setGroundSkill.SetActive(false);
            
            StartCoroutine(Fade(0));
        }
        
        
        
        

    }
    
    
    
    /// <summary>
    /// 淡入淡出动画
    /// </summary>
    /// <param name="targetAlpha"></param>
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
        }
    }
    
    
    
    /// <summary>
    /// 加载场景切换动画
    /// </summary>
    public void LoadSceneAnim()
    {
        //进度条加载
        loadSlider.DOValue(1, loadSceneDuration).SetEase(Ease.Linear);
    }


    
    /// <summary>
    /// 返回最初场景
    /// </summary>
    public void TransitionOriginScene()
    {
        
    }



    

    /// <summary>
    /// 旋转摄像机
    /// </summary>
    /// <returns></returns>
    // public IEnumerator RotateCamera()
    // {
    //     float timer = 0;
    //
    //     while (timer < 2)
    //     {
    //         timer += Time.deltaTime;
    //         virtualCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.Dutch += cameraRotateSpeed * Time.deltaTime;
    //         yield return null;
    //     }
    //     
    //     virtualCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.Dutch = 0;
    // }
}
