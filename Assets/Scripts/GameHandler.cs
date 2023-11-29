using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Panels;
using FMODUnity;
using FMOD.Studio;
using System;
public class GameHandler : MonoBehaviour
{
    public static GameHandler instance;
    public PlayerManager littlePlayerManager;
    public PlayerManager bigPlayerManager;
    public float gravity;

    public bool playerIsLittle = false;

    PauseMenu pauseMenu;
    [SerializeField] GameObject GameOverCanvas;

    MetaControls metaControls;

    public class OnPlayerSizeChangeArgs : EventArgs
    {
        public bool little;
    }
    public event System.EventHandler<OnPlayerSizeChangeArgs> OnPlayerSizeChange;
    bool isGameOver = false;
    public event System.EventHandler OnPlayerRestart;
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
        littlePlayerManager.GetComponent<PlayerHealth>().OnPlayerDeath += OnPlayerDeath;
        bigPlayerManager = PlayerManager.bigPlayerInstance;
        StartCoroutine("GameStart");
        musicEventInstance = AudioManager.instance.Play(Music);
        musicEventInstance.setVolume(GameSettings.instance.musicVolume);
        AudioManager.instance.SetFMODParameter("SFXVolume", GameSettings.instance.sfxVolume);
        AudioManager.instance.SetFMODParameter("MasterVolume", GameSettings.instance.masterVolume);
        
        // print out all player settings from PlayerPrefs in one line
        Debug.Log($"verticalLookSensitivity: {GameSettings.instance.verticalLookSensitivity}, horizontalLookSensitivity: {GameSettings.instance.horizontalLookSensitivity}, masterVolume: {GameSettings.instance.masterVolume}, sfxVolume: {GameSettings.instance.sfxVolume}, musicVolume: {GameSettings.instance.musicVolume}");
    }
    IEnumerator GameStart()
    {
        yield return null;
        SetActivePlayer(little: true, sendEventCalls: false);
    }
    private void OnEnable()
    {
        if(metaControls == null)
        {
            metaControls = new MetaControls();
            metaControls.GameHandler.PauseGame.performed += x=>pauseMenu.TogglePauseState();
            metaControls.GameHandler.GrowShrink.performed += x=>SetActivePlayer(!playerIsLittle, sendEventCalls:true);
            metaControls.GameHandler.Restart.performed += x=>RestartGame();
        }
        metaControls.Enable();
    }

    private void RestartGame()
    {
        if (isGameOver)
        {
            Time.timeScale = 1f;
            isGameOver = false;
            SetActivePlayer(false, false);
            GameOverCanvas.SetActive(false);
            OnPlayerRestart.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnDisable()
    {
        metaControls.Disable();
    }
    public void SetActivePlayer(bool little, bool sendEventCalls)
    {
        bigPlayerManager.StopProcesses();
        littlePlayerManager.StopProcesses();
        playerIsLittle = little;
        //if(bigPlayerManager == null)
        //{
        //    musicEventInstance.setParameterByName("MusicPlayerSize", 1);
        //    return;
        //}
        if (!little)
        {
            musicEventInstance.setParameterByName("MusicPlayerSize", 0);
            PlayerHealth.SyncPlayerHealth(littlePlayerManager.characterHealth as PlayerHealth, bigPlayerManager.characterHealth as PlayerHealth);
        }
        else
        {
            musicEventInstance.setParameterByName("MusicPlayerSize", 1);
            PlayerHealth.SyncPlayerHealth(bigPlayerManager.characterHealth as PlayerHealth, littlePlayerManager.characterHealth as PlayerHealth);

            littlePlayerManager.playerLocomotion.WarpPosition(Checkpoint.currentActiveCheckpoint.spawnPoint);
        }
        littlePlayerManager.gameObject.SetActive(little);
        bigPlayerManager.gameObject.SetActive(!little);
        if (sendEventCalls)
        {
            OnPlayerSizeChange.Invoke(this, new OnPlayerSizeChangeArgs { little = little });
        }
    }
    
    private void OnPlayerDeath(object sender, EventArgs e)
    {
        Time.timeScale = 0;
        GameOverCanvas.SetActive(true);
        isGameOver = true;
    }


    public void SetMusicVolume(float value)
    {
        FMODUnity.RuntimeManager.GetBus("bus:/BGM").setVolume(value);
    }
    
    public void SetSfxVolume(float value)
    {
        FMODUnity.RuntimeManager.GetBus("bus:/SFX").setVolume(value);
    }
    
    public void SetMasterVolume(float value)
    {
        FMODUnity.RuntimeManager.GetBus("bus:/").setVolume(value);
    }
}
