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
    private SpriteRenderer spriteRenderer;
    public Vector2 inputDirection;
    private CapsuleCollider2D coll;
    [Header("基本参数")]
    public float speed;
    private float runSpeed;
    private float walkSpeed;
    public float jumpForce;
    private Vector2 originalOffeset;
    private Vector2 originalSize;

    public bool isCrouch = false;
    private void Awake()
    {
        InputControl = new PlayerInputControl();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        coll = GetComponent<CapsuleCollider2D>();
        originalOffeset = coll.offset;
        originalSize = coll.size;
        
        InputControl.Gameplay.Jump.started += Jump;
        InputControl.Gameplay.RunButton.performed += RunStatsChenge;
        InputControl.Gameplay.RunButton.canceled += WalkStatsChenge;
        
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
        
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        //移动
        if (!isCrouch)
        {
            rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime,rb.velocity.y);
        }

        if (inputDirection.x != 0)
        {
            spriteRenderer.flipX = inputDirection.x > 0 ? false : true;
        }
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
    
}
