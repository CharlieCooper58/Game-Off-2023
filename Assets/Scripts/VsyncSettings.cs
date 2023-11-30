using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VsyncSettings : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Sync the frame rate to the screen's refresh rate
        QualitySettings.vSyncCount = 0;

        // Set target frame rate to the screens max refresh rate
        Application.targetFrameRate = -1;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
