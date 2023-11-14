using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager
{
    public InputHandler inputHandler;
    public PlayerCamera playerCamera;
    public PlayerAttacker playerAttacker;
    public PlayerLocomotion playerLocomotion;
    public PlayerInteractor playerInteractor;

    public bool isLittle;
    public static PlayerManager littlePlayerInstance;
    public static PlayerManager bigPlayerInstance;


    protected override void Awake()
    {
        if (isLittle)
        {
            if (littlePlayerInstance == null)
            {
                littlePlayerInstance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            if (bigPlayerInstance == null)
            {
                bigPlayerInstance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        base.Awake();
        inputHandler = GetComponent<InputHandler>();
        playerCamera = GetComponent<PlayerCamera>();
        playerAttacker = GetComponent<PlayerAttacker>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        playerInteractor = GetComponent<PlayerInteractor>();
        
    }
    protected override void Start()
    {
        base.Start();
        playerCamera.Initialize();
        playerLocomotion.Initialize();

        if (isLittle)
        {
            playerAttacker.Initialize();
        }
        else
        {

        }


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
