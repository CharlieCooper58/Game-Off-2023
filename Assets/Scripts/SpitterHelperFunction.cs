using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitterHelperFunction : MonoBehaviour
{
    SpitterAI spitter;
    private void Start()
    {
        spitter = GetComponentInParent<SpitterAI>();
    }

    public void SpitterAttack()
    {
        spitter.Attack();
    }
}
