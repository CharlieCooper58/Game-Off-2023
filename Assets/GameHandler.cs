using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public static GameHandler instance;
    public PlayerManager playerManager;
    public float gravity;
    private void Awake()
    {
        instance = this;
        Physics.gravity = new Vector3(0, -gravity, 0);
        playerManager = FindObjectOfType<PlayerManager>();
    }
}
