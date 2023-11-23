using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombardierHelperFunction : MonoBehaviour
{
    BombardierAI spitter;
    private void Start() {
        spitter = GetComponentInParent<BombardierAI>();
    }

    public void SpitterAttack() {
        spitter.Attack();
    }
}
