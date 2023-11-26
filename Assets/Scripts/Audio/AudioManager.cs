using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Audio Manager in the scene");
        }
        instance = this;
    }




    //A simple function to play once. I strongly recommend using Play() instead to keep track of your sounds.
    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }
    public EventInstance Play(EventReference sound, Vector3 worldPos) { 
        var evi = RuntimeManager.CreateInstance(sound);
        evi.set3DAttributes(RuntimeUtils.To3DAttributes(worldPos));
        evi.start();
        return evi;
    }
    public EventInstance Play(EventReference sound) {
        var evi = RuntimeManager.CreateInstance(sound);
        evi.start();
        return evi;
    }
    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        return eventInstance;
    }
    public void SetFMODParameter(string parameterName, float parameterValue)
    {
        RuntimeManager.StudioSystem.setParameterByName(parameterName, parameterValue);
    }
}
