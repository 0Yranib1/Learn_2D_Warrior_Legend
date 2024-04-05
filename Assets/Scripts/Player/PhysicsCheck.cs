using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    [Header("状态")]
    public bool isGround;

    [Header("检测参数")]
    public float checkRaduis;
    public LayerMask groundLayer;
    public Vector2 bottomOffset;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Check();
    }
    public void Check()
    {
        //检测地面
        isGround= Physics2D.OverlapCircle((Vector2)transform.position+bottomOffset, checkRaduis, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position+bottomOffset,checkRaduis);
    }
}
