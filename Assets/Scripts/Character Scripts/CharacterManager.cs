using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public CharacterLocomotion characterLocomotion;

    protected virtual void Awake()
    {
        characterLocomotion = GetComponent<CharacterLocomotion>();
    }

    protected virtual void Start()
    {
        characterLocomotion.Initialize();
    }

    private void FixedUpdate()
    {
        characterLocomotion.HandleAllMovement();
    }
}
