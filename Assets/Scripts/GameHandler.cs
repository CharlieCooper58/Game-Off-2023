using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Panels;

public class GameHandler : MonoBehaviour
{
    public static GameHandler instance;
    public PlayerManager littlePlayerManager;
    public PlayerManager bigPlayerManager;
    public float gravity;

    bool playerIsLittle = false;

    PauseMenu pauseMenu;

    MetaControls metaControls;
    private void Awake()
    {
        instance = this;
        Physics.gravity = new Vector3(0, -gravity, 0);
        pauseMenu = GetComponentInChildren<PauseMenu>();
    }
    private void Start()
    {
        littlePlayerManager = PlayerManager.littlePlayerInstance;
        bigPlayerManager = PlayerManager.bigPlayerInstance;
        StartCoroutine("GameStart");
    }
    IEnumerator GameStart()
    {
        yield return null;
        SetActivePlayer(little: false);
    }
    private void OnEnable()
    {
        if(metaControls == null)
        {
            metaControls = new MetaControls();
            metaControls.GameHandler.PauseGame.performed += x=>pauseMenu.TogglePauseState();
            metaControls.GameHandler.GrowShrink.performed += x=>SetActivePlayer(!playerIsLittle);
        }
        metaControls.Enable();
    }
    private void OnDisable()
    {
        metaControls.Disable();
    }
    public void SetActivePlayer(bool little)
    {
        playerIsLittle = little;
        if(bigPlayerManager == null)
        {
            return;
        }
        if (!little)
        {
            bigPlayerManager.transform.position = littlePlayerManager.transform.position;
        }
        else
        {
            littlePlayerManager.transform.position = bigPlayerManager.transform.position;
        }
        littlePlayerManager.gameObject.SetActive(little);
        bigPlayerManager.gameObject.SetActive(!little);
    }
}
