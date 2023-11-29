using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class AnimatedInteractable : Interactable
{
    Animator m_animator;
    private void Awake()
    {
        m_animator = GetComponent<Animator>();   
    }
    public override void OnInteract(PlayerInteractor interactor)
    {
        base.OnInteract(interactor);
        m_animator.SetTrigger("Interact");
    }
}
