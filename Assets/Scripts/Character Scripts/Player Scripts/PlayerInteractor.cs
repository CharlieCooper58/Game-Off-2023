using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    Interactable hovered;
    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), out hit))
        {
            Interactable interactableObject = hit.collider.GetComponent<Interactable>();
            if(interactableObject != null && interactableObject != hovered)
            {
                hovered = interactableObject;
                interactableObject.OnHoverEnter();
            }
        }
    }
}