using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static GameSettings instance;
    [Header("Gameplay Settings")]
    public float verticalLookSensitivity = 0.5f;
    public float horizontalLookSensitivity = 0.5f;

    [Header("Audio Settings")]
    public float masterVolume = 0.5f;
    public float sfxVolume = 0.5f;
    public float musicVolume = 0.5f;

    public const string VerticalSensitivityKey = "VerticalLookSensitivity";
    public const string HorizontalSensitivityKey = "HorizontalLookSensitivity";
    public const string MasterVolumeKey = "MasterVolume";
    public const string SFXVolumeKey = "SFXVolume";
    public const string MusicVolumeKey = "MusicVolume";

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        LoadSettings();
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat(VerticalSensitivityKey, verticalLookSensitivity);
        PlayerPrefs.SetFloat(HorizontalSensitivityKey, horizontalLookSensitivity);
        PlayerPrefs.SetFloat(MasterVolumeKey, masterVolume);
        PlayerPrefs.SetFloat(SFXVolumeKey, sfxVolume);
        PlayerPrefs.SetFloat(MusicVolumeKey, musicVolume);

        PlayerPrefs.Save();
    }

    public void LoadSettings()
    {
        if (PlayerPrefs.HasKey(VerticalSensitivityKey))
        {
            verticalLookSensitivity = PlayerPrefs.GetFloat(VerticalSensitivityKey);
        }

        if (PlayerPrefs.HasKey(HorizontalSensitivityKey))
        {
            horizontalLookSensitivity = PlayerPrefs.GetFloat(HorizontalSensitivityKey);
        }

        if (PlayerPrefs.HasKey(MasterVolumeKey))
        {
            masterVolume = PlayerPrefs.GetFloat(MasterVolumeKey);
        }

        if (PlayerPrefs.HasKey(SFXVolumeKey))
        {
            sfxVolume = PlayerPrefs.GetFloat(SFXVolumeKey);
        }

        if (PlayerPrefs.HasKey(MusicVolumeKey))
        {
            musicVolume = PlayerPrefs.GetFloat(MusicVolumeKey);
        }
    }

    public void ChangeSettingByName(string settingName, float value)
    {
        print("Changing "+ settingName);
        switch (settingName)
        {
            case VerticalSensitivityKey:
                verticalLookSensitivity = value;
                break;
            case HorizontalSensitivityKey:
                horizontalLookSensitivity = value;
                break;
            case MasterVolumeKey:
                masterVolume = value;
                break;
            case SFXVolumeKey:
                sfxVolume = value;
                break;
            case MusicVolumeKey:
                musicVolume = value;
                break;
            default:
                Debug.LogWarning("Setting not found: " + settingName);
                break;

        }
        SaveSettings() ;

    }
}
