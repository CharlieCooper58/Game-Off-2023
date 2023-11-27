using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    Interactable hovered;
    public float interactRange;
    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), out hit, interactRange))
        {
            Interactable interactableObject = hit.collider.GetComponent<Interactable>();
            if (interactableObject != null && interactableObject != hovered)
            {
                hovered = interactableObject;
                interactableObject.OnHoverEnter();
            }
            else if (interactableObject == null)
            {
                if (hovered != null)
                {
                    hovered.OnHoverExit();
                    hovered = null;
                }
            }
        }
    }

    public void InteractWithObject()
    {
        if(hovered != null)
        {
            hovered.OnInteract();
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
