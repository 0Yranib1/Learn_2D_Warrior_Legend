using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    private CapsuleCollider2D coll;
    [Header("状态")]
    public bool isGround;

    public bool touchLeftWall;
    public bool touchRightWall;

    [Header("检测参数")] 
    public bool manual;
    public float checkRaduis;
    public LayerMask groundLayer;
    public Vector2 bottomOffset;
    public Vector2 leftOffset;
    public Vector2 rightOffset;

    private void Awake()
    {
        coll = GetComponent<CapsuleCollider2D>();
        if (!manual)
        {
            rightOffset = new Vector2((coll.bounds.size.x + coll.offset.x) / 2, coll.bounds.size.y / 2);
            leftOffset = new Vector2(-rightOffset.x,rightOffset.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Check();
    }
    public void Check()
    {
        //检测地面
        isGround= Physics2D.OverlapCircle((Vector2)transform.position+ new Vector2(bottomOffset.x*transform.localScale.x,bottomOffset.y), checkRaduis, groundLayer);
        touchLeftWall=Physics2D.OverlapCircle((Vector2)transform.position+new Vector2(leftOffset.x*transform.localScale.x,leftOffset.y), checkRaduis, groundLayer);
        touchRightWall=Physics2D.OverlapCircle((Vector2)transform.position+new Vector2(rightOffset.x*transform.localScale.x,rightOffset.y), checkRaduis, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position+ new Vector2(bottomOffset.x*transform.localScale.x,bottomOffset.y), checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position+new Vector2(leftOffset.x*transform.localScale.x,leftOffset.y), checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position+new Vector2(rightOffset.x*transform.localScale.x,rightOffset.y), checkRaduis);
    }
}
