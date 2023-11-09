using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager
{
    public InputHandler inputHandler;
    public PlayerCamera playerCamera;
    public PlayerAttacker playerAttacker;
    public PlayerLocomotion playerLocomotion;
    protected override void Awake()
    {
        base.Awake();
        inputHandler = GetComponent<InputHandler>();
        playerCamera = GetComponent<PlayerCamera>();
        playerAttacker = GetComponent<PlayerAttacker>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
    }
    protected override void Start()
    {
        base.Start();
        playerCamera.Initialize();
        playerAttacker.Initialize();
        playerLocomotion.Initialize();
    }
    private void Update()
    {
        inputHandler.TickInput();
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        playerLocomotion.HandleAllMovement();
        inputHandler.TickCameraInput();
    }

}
