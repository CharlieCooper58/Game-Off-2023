using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager
{
    public InputHandler inputHandler;
    public PlayerCamera playerCamera;
    protected override void Awake()
    {
        base.Awake();
        inputHandler = GetComponent<InputHandler>();
        playerCamera = GetComponent<PlayerCamera>();
    }
    protected override void Start()
    {
        base.Start();
        playerCamera.Initialize();
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
