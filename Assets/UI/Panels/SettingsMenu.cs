using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Panels
{
    public class SettingsMenu : MonoBehaviour
    {
        private VisualElement _audioOptionsPanel;
        
        private Slider _masterVolumeSlider;
        private Slider _sfxVolumeSlider;
        private Slider _musicVolumeSlider;
        
        private VisualElement _displayOptionsPanel;
        private VisualElement _gameplayOptionsPanel;

        private VisualElement _main;
        private Button _backButton;
        private Button _audioButton;
        private Button _displayButton;
        private Button _gameplayButton;

        private UIDocument _document;

        private StartMenu _startMenu;
        private PauseMenu _pauseMenu;
        
        private void Start()
        {
            _document = GetComponent<UIDocument>();
          
            _main = _document.rootVisualElement.Q<VisualElement>("Main");
            
            _backButton = _document.rootVisualElement.Q<Button>("BackButton");
            _backButton.clicked += _backButton_clicked;
            
            _masterVolumeSlider = _document.rootVisualElement.Q<Slider>("MasterVolumeSlider");
            _masterVolumeSlider.value = GameSettings.instance.masterVolume;
            _masterVolumeSlider.RegisterValueChangedCallback(v => GameSettings.instance.ChangeSettingByName(GameSettings.MasterVolumeKey, _masterVolumeSlider.value));
            
            _musicVolumeSlider = _document.rootVisualElement.Q<Slider>("MusicVolumeSlider");
            _musicVolumeSlider.value = GameSettings.instance.musicVolume;
            _musicVolumeSlider.RegisterValueChangedCallback(v => GameSettings.instance.ChangeSettingByName(GameSettings.MusicVolumeKey, _musicVolumeSlider.value));

            _sfxVolumeSlider = _document.rootVisualElement.Q<Slider>("SFXVolumeSlider");
            _sfxVolumeSlider.value = GameSettings.instance.sfxVolume;
            _sfxVolumeSlider.RegisterValueChangedCallback(v => GameSettings.instance.ChangeSettingByName(GameSettings.SFXVolumeKey, _sfxVolumeSlider.value));

            var cameraLookXSlider = _document.rootVisualElement.Q<Slider>("HorizontalLook");
            cameraLookXSlider.value = GameSettings.instance.horizontalLookSensitivity;
            cameraLookXSlider.RegisterValueChangedCallback(v => GameSettings.instance.ChangeSettingByName(GameSettings.HorizontalSensitivityKey, _masterVolumeSlider.value));
            
            var cameraLookYSlider = _document.rootVisualElement.Q<Slider>("VerticalLook");
            cameraLookYSlider.value = GameSettings.instance.verticalLookSensitivity;
            cameraLookYSlider.RegisterValueChangedCallback(v => GameSettings.instance.ChangeSettingByName(GameSettings.VerticalSensitivityKey, _masterVolumeSlider.value));

            _startMenu = FindObjectOfType<StartMenu>();
            _pauseMenu = FindObjectOfType<PauseMenu>();
        }
        
        public void OpenDisplay()
        {
            _startMenu?.CloseDisplay();
            _pauseMenu?.CloseDisplay();
            _main.style.display = DisplayStyle.Flex;
        }
        
        public void CloseDisplay()
        {
            _startMenu?.OpenDisplay();
            _pauseMenu?.OpenDisplay();
            _main.style.display = DisplayStyle.None;
        }

        private void _backButton_clicked()
        {
            CloseDisplay();
        }
    }

}
