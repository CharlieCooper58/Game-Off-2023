using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    int animationStatePriority;
    ParticleSystem m_particleSystem;

    Animator m_animator;

    public void Initialize()
    {
        m_animator = GetComponent<Animator>();
    }

    public void PlayTargetAnimation(string animName, int statePriority)
    {
        if(statePriority > animationStatePriority)
        {
            animationStatePriority = statePriority;
            m_animator.Play(animName);
        }
    }

    public void ResetStatePriority()
    {
        animationStatePriority = 0;
    }

}
