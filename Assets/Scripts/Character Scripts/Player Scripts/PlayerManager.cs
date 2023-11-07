using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager
{
    public InputHandler inputHandler;
    public PlayerCamera playerCamera;
    public PlayerAttacker playerAttacker;
    protected override void Awake()
    {
        base.Awake();
        inputHandler = GetComponent<InputHandler>();
        playerCamera = GetComponent<PlayerCamera>();
        playerAttacker = GetComponent<PlayerAttacker>();
    }
    protected override void Start()
    {
        base.Start();
        playerCamera.Initialize();
        playerAttacker.Initialize();
    }
    private void Update()
    {
        inputHandler.TickInput();
    }
    private void LateUpdate()
    {
        inputHandler.TickCameraInput();
    }
}
