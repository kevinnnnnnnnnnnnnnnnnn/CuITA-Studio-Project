using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerUpStep : MonoBehaviour
{
    private Animator _anim;
    private Rigidbody2D _rb;
    
    [Header("组件")]
    [SerializeField] private PlayerMove playerMove; 
    

    [Header("参数")]
    [SerializeField]private bool isUpStepStop;//进入攀爬悬停状态
    [SerializeField] private bool isUpStep;//正在攀爬
    [SerializeField]private bool canUpStep;//允许攀爬的状态
    //[SerializeField]private float targetStepX;//切换攀爬状态后，直接使角色和梯子在相同x坐标
    [SerializeField] private float upStepSpeed;//攀爬速度
    
    
    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        UpStepStop();
        UpStep();
        
    }


    /// <summary>
    /// 判断是否可以进行攀爬
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Step"))
        {
            canUpStep = true;
        }
    }

    
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Step"))
        {
            canUpStep = false;
            
            //爬到梯子顶部离开Step时，重置Player状态
            isUpStep = false;
            isUpStepStop = false;   
            _anim.SetBool($"isUpStep", false);
            _anim.SetBool($"isUpStepStop", isUpStepStop);

            //启用重力
            _rb.gravityScale = 1;
            _rb.velocity = Vector2.Lerp(_rb.velocity, Vector2.zero, .01f);
            
            OpenAndColseMove();
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////


    /// <summary>
    /// 控制在爬梯子时禁用移动脚本
    /// </summary>
    private void OpenAndColseMove()
    {
        if (isUpStepStop)
        {
            playerMove.enabled = false;
        }
        else
        {
            playerMove.enabled = true;
        }
    }


    
    /// <summary>
    /// 在此处切换isUpStepStop状态，并切换爬梯子动画，同时将玩家移动至梯子x坐标,控制玩家移动脚本的开关
    /// </summary>
    private void UpStepStop()
    {
        //按w进入攀爬悬停状态
        if (canUpStep && Input.GetKeyDown(KeyCode.W))
        {
            //未攀爬时按下w换为攀爬状态
            isUpStepStop = true;
            _anim.SetBool($"isUpStepStop", isUpStepStop);

            //禁用重力
            _rb.gravityScale = 0;
            
            //设置玩家坐标到梯子，设置velocity为0，防止跳跃时点击攀爬，人物飞走
            transform.position = new Vector3(SetStepTargetManager.instance.stepPlayerTargetX, transform.position.y,
                                            transform.position.z);
            _rb.velocity = Vector2.zero;

            OpenAndColseMove();
        }
        //按s取消攀爬悬停状态
        else if (canUpStep && Input.GetKeyDown(KeyCode.S))
        {
            isUpStepStop = false;   
            _anim.SetBool($"isUpStepStop", isUpStepStop);

            //启用重力
            _rb.gravityScale = 1;
            
            OpenAndColseMove();
        }
    }

    
    /// <summary>
    /// 在此处切换isUpStep状态
    /// </summary>
    public void UpStep()
    {
        //持续按下w时，判断为正在爬梯子
        if (isUpStepStop && Input.GetKey(KeyCode.W))
        {
            isUpStep = true;
        }
        else
        {
            isUpStep = false;
        }

        //根据isUpStep状态，控制角色在梯子的速度
        if (isUpStep)
        {
            _rb.velocity = new Vector2(0, upStepSpeed);
        }
        else if(isUpStepStop && !isUpStep)
        {
            _rb.velocity = Vector2.zero;
        }

        
        _anim.SetBool($"isUpStep", isUpStep);
    }

}
