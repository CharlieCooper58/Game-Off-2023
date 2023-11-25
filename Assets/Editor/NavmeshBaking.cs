using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

[InitializeOnLoad]
public class AutoNavMeshBaker : MonoBehaviour
{
    static AutoNavMeshBaker()
    {
        // Subscribe to the play mode state change event
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            GameObject[] objectsToDisable = GameObject.FindGameObjectsWithTag("Baking Helper");
            foreach (GameObject obj in objectsToDisable)
            {
                obj.SetActive(false);
            }
        }
        else if(state == PlayModeStateChange.EnteredEditMode)
        {
            GameObject[] objectsToDisable = GameObject.FindGameObjectsWithTag("Baking Helper");
            foreach (GameObject obj in objectsToDisable)
            {
                obj.SetActive(true);
            }
        }
    }

}
