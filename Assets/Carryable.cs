using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carryable : Interactable
{
    [SerializeField] string textWhenSmall;
    [SerializeField] string textWhenLarge;
    [SerializeField] string textWhenCarried;

    [SerializeField] LayerMask carryMask;
    bool canInteract;
    bool beingCarried;
    Rigidbody rb;
    [SerializeField] float carryDistance;
    float curCarryDistance;
    Vector3 carryDirection;

    protected override void Start()
    {
        base.Start();
        GameHandler.instance.OnPlayerSizeChange += Instance_OnPlayerSizeChange;
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
        highlightText = textWhenLarge;
        canInteract = true;
    }
    private void Update()
    {
        if (beingCarried)
        {
            carryDirection = (transform.position-transform.parent.position).normalized;
            RaycastHit hit;
            if(Physics.Raycast(transform.parent.position, carryDirection, out hit, carryDistance, carryMask)){
                if(hit.collider.gameObject != gameObject)
                {
                    curCarryDistance = hit.distance * 0.9f;
                }
                else
                {
                    curCarryDistance = carryDistance;
                }
            }
            else
            {
                curCarryDistance = carryDistance;
            }
            transform.position = transform.parent.position + carryDirection * curCarryDistance;
        }
    }
    private void Instance_OnPlayerSizeChange(object sender, GameHandler.OnPlayerSizeChangeArgs e)
    {
        if (e.little)
        {
            highlightText = textWhenSmall;
            canInteract = false;
        }
        else
        {
            highlightText = textWhenLarge;
            canInteract = true;
        }
    }
    public override void OnInteract(PlayerInteractor interactor)
    {
        base.OnInteract(interactor);
        if (canInteract)
        {
            if (beingCarried)
            {
                transform.parent = null;
                rb.isKinematic = false;
                rb.useGravity = true;
                highlightText = textWhenLarge;
                beingCarried = false;
            }
            else
            {
                highlightText = textWhenCarried;
                transform.parent = interactor.carryParent;
                rb.isKinematic = true;
                rb.useGravity = false;
                beingCarried = true;
            }
        }
    }
}
