using System;
using System.Collections;
using System.Collections.Generic;
using CulTA;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;


public class TransitionManager : MonoBehaviour,ISaveable
{
    public static TransitionManager instance;

    public GameObject player;
    public CanvasGroup fadePanel;

    private bool isFade;
    public bool canTransition;
    [SerializeField] private float fadeDuration;

    public string currentScene;
    public float currentPosX;
    public float currentPosY;

    public string firstSceneName;

    private void Awake()
    {
        if(instance != null)
            Destroy(instance);
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

        SceneManager.LoadSceneAsync(firstSceneName, LoadSceneMode.Additive);
        player.transform.position = new Vector3(-5, 1.5f, 0);
        EventHandler.CallStartGameEvent(0);
    }


    /// <summary>
    /// 通过Transition的trigger判断是否进入传送门对应距离，是否可以传送
    /// </summary>
    /// <param name="value">传入的bool值</param>
    public void CanTransition(bool value)
    {
        canTransition = value;
    }


    private void OnEnable()
    {
        EventHandler.BeforeTransition += OnBeforeTransition;
        EventHandler.AfterTransition += OnAfterTransition;

        EventHandler.MenuStartNewGameEvent += MenuStartNewGame;
        EventHandler.MenuContinueGameEvent += MenuContinueGame;
    }

    private void OnDisable()
    {
        EventHandler.BeforeTransition -= OnBeforeTransition;
        EventHandler.AfterTransition -= OnAfterTransition;

        EventHandler.MenuStartNewGameEvent -= MenuStartNewGame;
        EventHandler.MenuContinueGameEvent -= MenuContinueGame;
    }

    private void Update()
    {

    }


    /// <summary>
    /// 开启协程加载场景并设置角色目标位置,重置当前场景中max和current光球数量
    /// </summary>
    /// <param name="from">当前场景</param>
    /// <param name="to">加载场景</param>
    /// <param name="targetX">目标场景中角色的目标x坐标</param>
    /// <param name="targetY">目标场景中角色的目标y坐标</param>
    public void Transition(string from, string to,float targetX, float targetY)
    {
        if (!isFade && canTransition)
        {
            StartCoroutine(TransitionToScene(from, to, targetX, targetY));
            
            //重置新场景光球数量
            CollectLightManager.instance.maxCollectLightNum = 0;
            CollectLightManager.instance.currentCollectLightNum = 0;
            
            GameManager.instance.currentScenePlayerPosX = targetX;
            GameManager.instance.currentScenePlayerPosY = targetY;
        }
    }


    
    /// <summary>
    /// 场景转换协程
    /// </summary>
    /// <param name="from">当前场景</param>
    /// <param name="to">目标场景</param>
    /// <returns></returns>
    private IEnumerator TransitionToScene(string from,string to,float targetX, float targetY)
    {
        //开启Transition前的事件
        EventHandler.CallBeforeTransition();
        
        Fade(1);
        yield return Fade(1);//1变黑

        if(from != string.Empty)//加载最初场景时，from为空，不需要卸载场景
        {
            yield return SceneManager.UnloadSceneAsync(from);
        }
        yield return SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive);
        
        //返回刚加载的新场景，并设置此场景为活动场景
        Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene (newScene);
        
        //开启Transition后的事件
        EventHandler.CallAfterTransition(targetX, targetY);
        
        if(SceneManager.GetActiveScene().name == "Persistent" || SceneManager.GetActiveScene().name == "Menu")
            PlayerLightManager.instance.playerLight.GetComponent<Light2D>().intensity = PlayerLightManager.instance.playerMenuLight;
        else
            PlayerLightManager.instance.playerLight.GetComponent<Light2D>().intensity = PlayerLightManager.instance.playerFirstLight;
        
        
        yield return Fade(0);//0变透明
    }
    
    
    /// <summary>
    /// 控制场景渐入渐出
    /// </summary>
    /// <param name="targetAlpha">要变化的目标alpha值</param>
    /// <returns></returns>
    private IEnumerator Fade(float targetAlpha)
    {
        isFade = true;

        //计算fade速度 
        float speed = Mathf.Abs((fadePanel.alpha - targetAlpha) / fadeDuration);

        while (!Mathf.Approximately(fadePanel.alpha, targetAlpha))
        {
            fadePanel.alpha = Mathf.MoveTowards(fadePanel.alpha, targetAlpha, speed * Time.deltaTime);
            yield return null;
        }

        isFade = false;
    }



    /// <summary>
    /// 注册到事件中，在场景切换前调用，Save当前数据，开启Panel
    /// </summary>
    public void OnBeforeTransition()
    {
        currentScene = SceneManager.GetActiveScene().name;
        var position = player.transform.position;
        currentPosX = position.x;
        currentPosY = position.y;
        
        SaveLoadManager.instance.Save();
        fadePanel.gameObject.SetActive(true);
    }
    
    /// <summary>
    /// 注册到事件中，在场景切换后调用，调整角色位置和反转，关闭Panel
    /// </summary>
    /// <param name="targetX">目标x</param>
    /// <param name="targetY">目标y</param>
    public void OnAfterTransition(float targetX, float targetY)
    {
        SaveLoadManager.instance.Load();
        
        //场景转换后关闭Panel，防止遮挡其他UI
        fadePanel.gameObject.SetActive(false);
        
        player.transform.position = new Vector3(targetX, targetY + 1f, player.transform.position.z);
        SpriteRenderer spr = player.GetComponent<SpriteRenderer>();
        spr.flipX = !spr.flipX;
    }


    /// <summary>
    /// 注册到事件中，在菜单开始新游戏时调用，切换到最初的场景
    /// </summary>
    /// <param name="toScene"></param>
    public void MenuStartNewGame(string toScene)
    {
        DoorOpenManager.instance.isOpenDoorDict.Clear();
        CollectLightManager.instance.isLightTokenDict.Clear();
        
        canTransition = true;
        Transition("Menu",toScene, -10, -2);
        canTransition = false;
    }

    
    /// <summary>
    /// 注册到事件中，在菜单继续游戏时调用，切换到目标场景
    /// </summary>
    /// <param name="toScene"></param>
    /// <param name="targetX"></param>
    /// <param name="targetY"></param>
    public void MenuContinueGame(string toScene, float targetX, float targetY)
    {
        canTransition = true;
        Transition("Menu", toScene, targetX, targetY);
        canTransition = false;
    }

    public GameSaveData GenerateSaveData()
    {
        GameSaveData saveData = new GameSaveData();

        saveData.currentSceneName = this.currentScene;
        saveData.playerPosX = this.currentPosX;
        saveData.playerPosY = this.currentPosY;

        return saveData;
    }

    public void ReStoreGameDate(GameSaveData saveData)
    {
        this.currentScene = saveData.currentSceneName;
        player.transform.position = new Vector2(saveData.playerPosX, saveData.playerPosY);
    }
}
