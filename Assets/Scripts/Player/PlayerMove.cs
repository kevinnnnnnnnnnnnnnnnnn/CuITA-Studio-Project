using CulTA;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D _rb;
    private BoxCollider2D _coll;
    private SpriteRenderer _spr;
    private Animator _anim;
    
    private float dirX = 0;//水平方向移动

    private bool isMoving;//是否在移动
    private bool isJumping; //是否在跳跃
    
 
    [SerializeField] private float moveSpeed = 7f;//移动速度
    [SerializeField] private float jumpForce = 7f;//跳跃力
    [SerializeField] private LayerMask groundLayer;//判断是否可以跳跃的地面

    private float _originJumpForce;

    void Start()
    {
 
    }


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _coll = GetComponent<BoxCollider2D>();
        _spr = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
        
        _originJumpForce = jumpForce;
    }


    private void Update()
    {
        Move();
        ChangeAnim();
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Hole")
        {
            jumpForce = 20;
        }
        else
        {
            return;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Hole")
        {
            jumpForce = _originJumpForce;
        }
        else
        {
            return;
        }
    }


    /// <summary>
    /// 角色移动和跳跃
    /// </summary>
    private void Move()
    {
        //移动
        dirX = Input.GetAxisRaw("Horizontal");
        _rb.velocity = new Vector2(dirX * moveSpeed, _rb.velocity.y);
        
        if(dirX != 0 && !isJumping)
            isMoving = true;
        else 
            isMoving = false;

        /////////////////////////////////////////////////////////////////////////////////
        //跳跃
        if (Input.GetButton("Jump") && IsGrounded())
        {
            var sample = GameApplication.BuiltInResources.GetSampleByName("jump");
            AudioSource.PlayClipAtPoint(sample, Vector3.zero);

            isJumping = true;
            _rb.velocity = new Vector3(_rb.velocity.x, jumpForce, 0);
        }
        else if(IsGrounded())
        {
            isJumping = false;
        }
        
        /////////////////////////////////////////////////////////////////////////////////
        //设置角色的朝向
        if (dirX > 0)
        {
            _spr.flipX = false;
        }
        else if (dirX < 0)
        {
            _spr.flipX = true;
        }
    }

    private bool IsGrounded()
    {
        //return Physics2D.Raycast(_coll.bounds.center, Vector2.down, 0.8f, groundLayer);     
        return Physics2D.BoxCast(_coll.bounds.center, new Vector2(0.5f,0.5f), 0f, Vector2.down, 0.5f, groundLayer);
    }
    
    
    private void ChangeAnim()
    {
        _anim.SetBool($"isMoving", isMoving);
        _anim.SetBool($"isJumping", isJumping);
    }
}
