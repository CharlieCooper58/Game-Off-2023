using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiltedMushroom : MonoBehaviour
{
    BouncePad m_mushPad;
    private void Awake()
    {
        m_mushPad = GetComponent<BouncePad>();
        m_mushPad.enabled = false;
    }
    public void OnMushroomWatered()
    {
        GetComponent<Animator>().Play("MushroomGrow");
    }
}
