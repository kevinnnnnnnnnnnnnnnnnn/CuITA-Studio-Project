using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;


public class PlayerControllor : MonoBehaviour
{
    
    private Rigidbody2D _rb;
    private AudioSource _as;
    private Animator _anim;
    private SpriteRenderer _sr;
    
    public ParticleSystem transitionParticles;
    
    public AudioClip jumpSound;
    public AudioClip jumpGoalSound;

    public Vector2 step = new Vector2(4.25f, 7.5f);
    
    public int inputIndex;
    public float jumpForce;

    public bool isJumping;
    public bool limitMove;

    public LayerMask goalLayer;

    private int _jumpCount = 0;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
        //_as = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isJumping)
            return;
        if (limitMove)
            return;

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            inputIndex = 1;
            _sr.flipX = false;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            inputIndex = -1;
            _sr.flipX = true;
        }
    }

    private void FixedUpdate()
    {
        Jump();
        ChangeAnim();
    }

    public void Jump()
    {
        if (inputIndex != 0 && !isJumping)
        {
            _jumpCount++;
            
            _rb.DOJump(_rb.position + new Vector2(step.x * inputIndex, step.y), jumpForce, 1, 0.4f)
                .SetEase(Ease.OutCubic).OnPlay(() => isJumping = true).OnComplete(() => isJumping = false);
            inputIndex = 0;
        }
    }

    public void ChangeAnim()
    {
        _anim.SetBool($"isJumping", isJumping);
    }
    
    
    
    /// <summary>
    /// 玩家触碰到传送方块后开启Trigger传送，以及触发获取技能
    /// </summary>
    /// <param name="other"></param>
    private async void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Transition"))
        {
            limitMove = true;
            
            await Task.Delay(300);
            
            transitionParticles.gameObject.SetActive(true);
            transitionParticles.Play();
            
            await Task.Delay(1400);
            
            transitionParticles.gameObject.SetActive(false);
            
            //Debug.Log("trigger");
            SameSceneTransitionManager.instance.TransitionSameScene(InitGroundManager.instance.groundCountNew,
                SameSceneTransitionManager.instance.playerTargetPosition);

            await Task.Delay(4000);
            
            limitMove = false;
        }


        
        //获取技能的Trigger
        if (other.CompareTag("GetSkills"))
        {
            GetSkillsManager.instance.canGetSetGroundSkill = true;
            GetSkillsManager.instance.GetSetGroundSkill();
        }

        if (other.CompareTag("TransitionOther"))
        {
            // limitMove = true;
            //
            // await Task.Delay(6700);
            //
            // limitMove = false;
        }
    }
    
    
    
}
