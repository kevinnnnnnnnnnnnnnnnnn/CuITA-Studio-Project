using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 挂在Player身上，控制Player爬梯子时上平台
/// </summary>
public class PlayerUpStepToPlatform : MonoBehaviour
{
    private Rigidbody2D _rb;


    private float _currentVelocityY;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }


    /// <summary>
    /// 与梯子上的平台发生碰撞，获取当前的玩家速度
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("StepPlatform"))
        {
            Debug.Log("进入StepPlatform");
            _currentVelocityY = _rb.velocity.y;
            
            //当前速度为正，即在平台下方向上运动
            if (_currentVelocityY > 0)
            {
                Debug.Log("当前速度为正，即在平台下方向上运动");
                other.gameObject.GetComponent<Collider2D>().enabled = false;
            }
            else
            {
                other.gameObject.GetComponent<Collider2D>().enabled = true;
            }
        }
        
        
    }


}
