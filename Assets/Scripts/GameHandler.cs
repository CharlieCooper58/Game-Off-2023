using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Panels;
using FMODUnity;
using FMOD.Studio;
public class GameHandler : MonoBehaviour
{
    public static GameHandler instance;
    public PlayerManager littlePlayerManager;
    public PlayerManager bigPlayerManager;
    public float gravity;

    bool playerIsLittle = false;

    PauseMenu pauseMenu;

    MetaControls metaControls;
    [SerializeField] private EventReference Music;

    EventInstance musicEventInstance;
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
        musicEventInstance = AudioManager.instance.Play(Music);
        musicEventInstance.setVolume(.75f);
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
            musicEventInstance.setParameterByName("MusicPlayerSize", 1);
            return;
        }
        if (!little)
        {
            musicEventInstance.setParameterByName("MusicPlayerSize", 0);

            bigPlayerManager.transform.position = littlePlayerManager.transform.position;
        }
        else
        {
            musicEventInstance.setParameterByName("MusicPlayerSize", 1);

            littlePlayerManager.transform.position = bigPlayerManager.transform.position;
        }
        littlePlayerManager.gameObject.SetActive(little);
        bigPlayerManager.gameObject.SetActive(!little);
    }
}
