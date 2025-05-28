using CulTA;
using UnityEngine;

public class CollectLight : MonoBehaviour
{
    //光球是否被拾取,用于在CollectLightManager中记录并存档
    [SerializeField] private bool isLightToken;
    //Dictionary<string, bool> isLightTokenDict = new Dictionary<string, bool>();
    
    
    /// <summary>
    /// 场景开始时，每个光球都初始化数量，使max+1,计算光球总数
    /// </summary>
    private void Awake()
    {
        CollectLightManager.instance.UpdateMaxCollectLightNum();
        isLightToken = false;
        
        CollectLightManager.instance.collectLightList.Add(this);
        
        CollectLightManager.instance.isLightTokenDict.TryAdd(gameObject.name, isLightToken);
        
    }


    private void Start()
    {
        //在start中保存isLightToken到字典中，否则false值不会被记录
        CollectLightManager.instance.isLightTokenDict[gameObject.name] = isLightToken;
    }


    /// <summary>
    /// 接触光球之后销毁光球，更新已收集的光球数量
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isLightToken = true;
            CollectLightManager.instance.isLightTokenDict[gameObject.name] = isLightToken;
            
            gameObject.SetActive(false);
            CollectLightManager.instance.UpdateCurrentCollectLightNum();

            // play sfx
            var sample = GameApplication.BuiltInResources.GetSampleByName("pickupCoin");
            MonoAudioPlayer.PlayOneShot(sample);
        }
    }


    private void Update()
    {
        //ControlLight();
    }


    public void ControlLight()
    {
        if (!isLightToken)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    
    

    private void OnDestroy()
    {
        CollectLightManager.instance.collectLightList.Remove(this);
    }
}
