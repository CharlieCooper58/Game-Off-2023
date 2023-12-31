using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class Door : Interactable
{
    Animator m_animator;
    [SerializeField] EventReference doorOpenSound;
    [SerializeField] EventReference doorCloseSound;
    bool closed;
    private void Awake()
    {
        m_animator = GetComponent<Animator>();
        closed = true;
    }
    public override void OnInteract(PlayerInteractor interactor)
    {
        base.OnInteract(interactor);
        m_animator.SetTrigger("Interact");
        if (closed)
        {
            AudioManager.instance.Play(doorOpenSound, PlayerManager.bigPlayerInstance.transform.position);
            closed = false;
        }
        else
        {
            AudioManager.instance.Play(doorCloseSound, PlayerManager.bigPlayerInstance.transform.position);
            closed = true;
        }
    }
}
