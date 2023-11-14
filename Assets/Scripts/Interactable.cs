using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable: MonoBehaviour
{
    Material highlightMaterial;

    void Start()
    {
        Renderer m_renderer = GetComponent<Renderer>();
        for (int i = 0; i < m_renderer.materials.Length; i++)
        {
            if (m_renderer.materials[i].HasProperty("_Thickness"))
            {
                print("Found it");
                highlightMaterial = m_renderer.materials[i];
                break;
            }
        }
        if (highlightMaterial != null)
        {
            highlightMaterial.SetFloat("_Thickness", 0);
        }
    }
    public virtual void OnInteract()
    {
        Debug.Log("He clicked me!");
    }
    public virtual void OnHoverEnter() 
    {
        Debug.Log("It's not that it's not working");
        highlightMaterial?.SetFloat("_Thickness", .975f);
    }
    public virtual void OnHoverExit() 
    {
        highlightMaterial?.SetFloat("_Thickness", 0);
    }

}
