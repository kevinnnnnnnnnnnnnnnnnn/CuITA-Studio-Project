using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject doorLight;
    [SerializeField] private GameObject transitionButton;
    
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
        door.SetActive(false);
        doorLight.SetActive(false);
        doorLight.GetComponent<Light2D>().intensity = 0;
        
        
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
        if (CollectLightManager.instance.currentCollectLightNum == CollectLightManager.instance.maxCollectLightNum)
        {
            door.SetActive(true);
            _isDoorOpen = true;
            
            DoorOpenManager.instance.isOpenDoorDict[this.name] = _isDoorOpen;
        }
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
                PlayerLightManager.instance.playerLight.GetComponent<Light2D>().intensity =
                    PlayerLightManager.instance.playerFirstLight;
            }
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        transitionButton.SetActive(false);
        StopCoroutine(_openDoorLightCoroutine);
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
