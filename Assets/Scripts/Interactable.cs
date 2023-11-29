using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable: MonoBehaviour
{
    Material highlightMaterial;
    public string highlightText;
    protected virtual void Start()
    {
        Renderer m_renderer = GetComponent<Renderer>();
        for (int i = 0; i < m_renderer.materials.Length; i++)
        {
            if (m_renderer.materials[i].HasProperty("_Thickness"))
            {
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
    }
    public virtual void OnHoverEnter() 
    {
        highlightMaterial?.SetFloat("_Thickness", .975f);
    }
    public virtual void OnHoverExit() 
    {
        print("Exit");
        highlightMaterial?.SetFloat("_Thickness", 0);
    }
}
