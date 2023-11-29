using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carryable : Interactable
{
    [SerializeField] string textWhenSmall;
    [SerializeField] string textWhenLarge;
    [SerializeField] string textWhenCarried;

    bool canInteract;
    bool beingCarried;
    Rigidbody rb;


    protected override void Start()
    {
        GameHandler.instance.OnPlayerSizeChange += Instance_OnPlayerSizeChange;
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    private void Instance_OnPlayerSizeChange(object sender, GameHandler.OnPlayerSizeChangeArgs e)
    {
        if (e.little)
        {
            highlightText = textWhenSmall;
        }
        else
        {
            highlightText = textWhenLarge;
        }
    }
    public override void OnInteract()
    {
        base.OnInteract();
        if (beingCarried)
        {

        }
    }
}
