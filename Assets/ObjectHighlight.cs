using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHighlight : MonoBehaviour
{
    Material highlightMaterial;
    // Start is called before the first frame update
    void Start()
    {
        Renderer m_renderer = GetComponent<Renderer>();
        for(int i = 0; i < m_renderer.materials.Length; i++)
        {
            if (m_renderer.materials[i].HasProperty("Thickness"))
            {
                print("Found it");
                highlightMaterial = m_renderer.materials[i];
                break;
            }
        }
        if(highlightMaterial != null)
        {
            highlightMaterial.SetFloat("Thickness", 0);
        }
    }

    public void SetHighlight()
    {
        highlightMaterial.SetFloat("Thickness", 1);
    }
    public void EndHighlight()
    {
        highlightMaterial.SetFloat("Thickness", 0);
    }
}
