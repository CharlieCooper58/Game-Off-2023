using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using UnityEngine.SceneManagement;
public class IntroCinematic : MonoBehaviour
{
    [SerializeField] EventReference introCinematicVO;
    // Start is called before the first frame update
    void Start()
    {
        var evi = RuntimeManager.CreateInstance(introCinematicVO);
        evi.start();
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Scenes/Main");
    }

}
