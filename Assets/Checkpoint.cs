using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public static Checkpoint currentActiveCheckpoint;
    public bool isFirstCheckpoint;
    public bool isLastCheckpoint;
    public Vector3 spawnPoint;
    private Vector3 spawnOffset = 2 * Vector3.up;

    public Arena myArena;
    private void Awake()
    {
        if (isFirstCheckpoint)
        {
            currentActiveCheckpoint = this;
        }
        spawnPoint = spawnOffset+transform.position;
    }

    public bool TrySetCheckpoint()
    {
        if(myArena == null || myArena.arenaIsComplete)
        {
            currentActiveCheckpoint = this;
            return true;
        }
        return false;
    }
}
