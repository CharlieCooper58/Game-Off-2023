using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public static GameHandler instance;
    public PlayerManager playerManager;
    private void Awake()
    {
        instance = this;
    }
}
