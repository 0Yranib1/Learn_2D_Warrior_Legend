using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerInputControl InputControl;
    public Vector2 inputDirection;
    private void Awake()
    {
        InputControl = new PlayerInputControl();
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
}
