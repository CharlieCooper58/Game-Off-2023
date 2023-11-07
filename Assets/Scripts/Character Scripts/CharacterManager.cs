using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public CharacterLocomotion characterLocomotion;
    public CharacterHealth characterHealth;
    protected virtual void Awake()
    {
        characterLocomotion = GetComponent<CharacterLocomotion>();
        characterHealth = GetComponent<CharacterHealth>();
    }

    protected virtual void Start()
    {
        characterLocomotion.Initialize();
        //characterHealth.Initialize();
    }

    private void FixedUpdate()
    {
        characterLocomotion.HandleAllMovement();
    }
}
