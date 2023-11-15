using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedInteractable : Interactable
{
    Animator m_animator;
    private void Awake()
    {
        m_animator = GetComponent<Animator>();   
    }
    public override void OnInteract()
    {
        base.OnInteract();
        m_animator.SetTrigger("Interact");
    }
}
