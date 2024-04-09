using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInputControl InputControl;
    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    public Vector2 inputDirection;
    private CapsuleCollider2D coll;
    private PlayerAnimation playerAnimation;
    [Header("物理材质")] 
    public PhysicsMaterial2D normal;
    public PhysicsMaterial2D wall;
    [Header("基本参数")]
    public float speed;
    private float runSpeed;
    private float walkSpeed;
    public float jumpForce;
    private Vector2 originalOffeset;
    private Vector2 originalSize;
    [Header("状态")]
    public bool isCrouch = false;
    public bool isHurt;
    public float hurtForce;
    public bool isDead;
    public bool isAttack;
    private int faceDirection=1;
    private void Awake()
    {
        InputControl = new PlayerInputControl();
        rb = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
        
        coll = GetComponent<CapsuleCollider2D>();
        originalOffeset = coll.offset;
        originalSize = coll.size;
        
        InputControl.Gameplay.Jump.started += Jump;
        InputControl.Gameplay.RunButton.performed += RunStatsChenge;
        InputControl.Gameplay.RunButton.canceled += WalkStatsChenge;
        InputControl.Gameplay.Attack.started += PlayerAttack;   
        physicsCheck = GetComponent<PhysicsCheck>();
        
        runSpeed = speed*3f;
        walkSpeed = speed;
    }

    private void OnEnable()
    {
        
        InputControl.Enable();
    }

    private void OnDisable()
    {
        InputControl.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        inputDirection = InputControl.Gameplay.Move.ReadValue<Vector2>();
        CheckState();
        
    }

    private void FixedUpdate()
    {
        if (!isHurt && !isAttack)
        {
            Move();
        }
    }

    public void Move()
    {
        //移动
        if (!isCrouch )
        {
            rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime,rb.velocity.y);
        }

        if (inputDirection.x > 0)
            faceDirection = 1;
        if (inputDirection.x < 0)
            faceDirection = -1;
        
        transform.localScale = new Vector3(faceDirection, 1, 1);
        
        //下蹲
        isCrouch = inputDirection.y < -0.5f && physicsCheck.isGround;
        if (isCrouch)
        {
            //修改碰撞体
            coll.offset = new Vector2(originalOffeset.x,0.72f);
            coll.size = new Vector2(originalSize.x,1.4f);
        }
        else
        {
            //还原碰撞体参数
            coll.size = originalSize;
            coll.offset = originalOffeset;
        }
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        if (physicsCheck.isGround)
        {
            rb.AddForce(transform.up*jumpForce,ForceMode2D.Impulse);
        }

    }
    private void RunStatsChenge(InputAction.CallbackContext obj)
    {
        if (physicsCheck.isGround)
        {
            speed = runSpeed;
        }
    }
    private void WalkStatsChenge(InputAction.CallbackContext obj)
    {
        if (physicsCheck.isGround)
        {
            speed = walkSpeed;
        }
    }

    private void PlayerAttack(InputAction.CallbackContext obj)
    {
        playerAnimation.PlayerAttack();
        isAttack = true;
    }
    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2(transform.position.x - attacker.position.x, 0).normalized;
        rb.AddForce(dir*hurtForce,ForceMode2D.Impulse);
    }

    public void playerDead()
    {
        isDead = true;
        InputControl.Gameplay.Disable();
    }

    private void CheckState()
    {
        coll.sharedMaterial = physicsCheck.isGround ? normal : wall;
    }
}
