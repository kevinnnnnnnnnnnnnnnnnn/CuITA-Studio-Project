using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DoorOpen : MonoBehaviour
{
    public static DoorOpen instance;
    
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject doorLight;
    [SerializeField] public GameObject transitionButton;
    
    private Coroutine _openDoorLightCoroutine;

    private bool _isOpenDoorLight;
    private bool _isDoorOpen;
    [SerializeField] private float openDoorLightTime;
    [SerializeField] private float doorLightIntensity;
    
    
    /// <summary>
    /// 初始化时关闭传送门
    /// </summary>
    private void Awake()
    {
        instance = this;
        
        door.SetActive(false);
        doorLight.SetActive(false);
        doorLight.GetComponent<Light2D>().intensity = 0;

        if (DoorOpenManager.instance == null)
        {
            Debug.Log("door open manager is null");
            return;
        }
        DoorOpenManager.instance.isOpenDoorDict.TryAdd(this.name, _isDoorOpen);

        if (DoorOpenManager.instance.isOpenDoorDict[this.name])
        {
            door.SetActive(true);
            doorLight.GetComponent<Light2D>().intensity += doorLightIntensity;
            doorLight.SetActive(true);
        }
        
        //TODO：门的active位置需调整
    }


    private void Start()
    {
        DoorOpenManager.instance.isOpenDoorDict[this.name] = _isDoorOpen;
    }


    private void Update()
    {
        OpenDoor();
    }


    /// <summary>
    /// 比较当前光球和最大光球，判断是否开启传送门
    /// </summary>
    public void OpenDoor()
    {
        if (CollectLightManager.instance.currentCollectLightNum == CollectLightManager.instance.maxCollectLightNum 
                    && CollectLightManager.instance.maxCollectLightNum != 0)
        {
            OpenDirectly();

            DoorOpenManager.instance.isOpenDoorDict[this.name] = _isDoorOpen;
        }
    }

    public void OpenDirectly()
    {
        door.SetActive(true);
        _isDoorOpen = true;
    }


    //使用协程，延迟一定时间后再打开传送门光源
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _openDoorLightCoroutine = StartCoroutine(OpenDoorLight());
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //当协程中打开传送门光源且此时传送门按钮未激活时，则激活传送门按钮
            if (_isOpenDoorLight && !transitionButton.activeSelf)
            {
                transitionButton.SetActive(true);
                
                //当激活传送门光源时，将玩家光源的恢复到最初值
                if(CollectLightManager.instance.currentCollectLightNum == CollectLightManager.instance.maxCollectLightNum)
                    PlayerLightManager.instance.playerLight.GetComponent<Light2D>().intensity =
                        PlayerLightManager.instance.playerFirstLight;
            }
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        transitionButton.SetActive(false);
        StopCoroutine(_openDoorLightCoroutine);

        doorLight.GetComponent<Light2D>().intensity = doorLightIntensity;
    }


    /// <summary>
    /// 打开传送门光源
    /// </summary>
    /// <returns></returns>
    IEnumerator OpenDoorLight()
    {
        yield return new WaitForSeconds(0.2f);
        
        doorLight.SetActive(true);        
        _isOpenDoorLight = true;      
          
        
        //实现传送门亮光的渐变过程
        while (doorLight.GetComponent<Light2D>().intensity < doorLightIntensity)
        {
            doorLight.GetComponent<Light2D>().intensity += Time.deltaTime / openDoorLightTime;
            yield return null;
        }
    }
}