using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public CharacterHealth characterHealth;
    public AnimationHandler animationHandler;
    protected virtual void Awake()
    {
        characterHealth = GetComponent<CharacterHealth>();
        animationHandler = GetComponentInChildren<AnimationHandler>();
    }

    protected virtual void Start()
    {
        //characterHealth.Initialize();
        if(animationHandler != null)
            animationHandler.Initialize();
    }

    protected virtual void FixedUpdate()
    {
    }
}
