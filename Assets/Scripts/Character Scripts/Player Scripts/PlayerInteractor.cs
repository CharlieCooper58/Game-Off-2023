using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    PlayerUIController uiController;
    Interactable hovered;
    public float interactRange;
    [SerializeField] LayerMask interactMask;

    [SerializeField] public Transform carryParent;
    private void Awake()
    {
        uiController = GetComponent<PlayerUIController>();
    }
    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), out hit, interactRange, interactMask))
        {
            Interactable interactableObject = hit.collider.GetComponent<Interactable>();
            if (interactableObject != null && interactableObject != hovered)
            {
                hovered = interactableObject;
                interactableObject.OnHoverEnter();
                if(interactableObject.highlightText != null)
                {
                    uiController.SetCurrentText(interactableObject.highlightText);
                }
            }
            else if (interactableObject == null)
            {
                if (hovered != null)
                {
                    hovered.OnHoverExit();
                    hovered = null;
                    uiController.StopShowingText();
                }
            }
        }
        else if(hovered != null)
        {
            hovered.OnHoverExit();
            hovered = null;
            uiController.StopShowingText();
        }
    }

    public void InteractWithObject()
    {
        if(hovered != null)
        {
            hovered.OnInteract(this);
            if (isActiveAndEnabled)
            {
                uiController.SetCurrentText(hovered.highlightText);
            }
        }
    }
    private void OnDisable()
    {
        if (hovered != null)
        {
            hovered.OnHoverExit();
            hovered = null;
        }
    }
}
