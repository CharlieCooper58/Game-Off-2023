using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;
using FMOD.Studio;
public class CreditsController : MonoBehaviour
{
    public EventReference music;
    private void Start()
    {
        
    }


    public void ReturnToMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
