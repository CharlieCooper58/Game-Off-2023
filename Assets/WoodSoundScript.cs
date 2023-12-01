using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class WoodSoundScript : MonoBehaviour
{
    [SerializeField] EventReference thunk;

    public void PlayWoodThunk()
    {
        AudioManager.instance.Play(thunk, transform.position);
    }
}
