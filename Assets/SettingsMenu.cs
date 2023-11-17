using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Panels
{
    public class SettingsMenu : MonoBehaviour
    {
        
        [SerializeField] private VisualTreeAsset _audioOptions;
        private VisualElement _audioOptionsPanel;
        //private Slider _masterVolumeSlider;
        //private Slider _sfxVolumeSlider;
        //private Slider _musicVolumeSlider;
        [SerializeField] private VisualTreeAsset _displayOptions;
        private VisualElement _displayOptionsPanel;
        [SerializeField] private VisualTreeAsset _gameplayOptions;
        private VisualElement _gameplayOptionsPanel;
        private enum CurrentOpenPanel
        {
            audio,
            display,
            gameplay
        }
        private CurrentOpenPanel _currentOpenPanel;
        private VisualElement _main;
        private Button _backButton;
        private Button _applyButton;
        private Button _audioButton;
        private Button _displayButton;
        private Button _gameplayButton;

        private VisualElement _optionsDisplayPanel;

        private UIDocument _document;

        StartMenu startMenu;
        PauseMenu pauseMenu;
        private void Start()
        {
            _document = GetComponent<UIDocument>();
            _main = _document.rootVisualElement.Q<VisualElement>("Main");
            _optionsDisplayPanel = _document.rootVisualElement.Q<VisualElement>("OptionsSpace");

            _backButton = _document.rootVisualElement.Q<Button>("BackButton");
            _backButton.clicked += _backButton_clicked;
            _applyButton = _document.rootVisualElement.Q<Button>("ApplyButton");

            _audioButton = _document.rootVisualElement.Q<Button>("Audio");
            _audioButton.clicked += _audioButton_clicked;
            _audioOptionsPanel = _audioOptions.CloneTree();
            Slider masterVolumeSlider = _audioOptionsPanel.Q<Slider>("MasterVolumeSlider");
            masterVolumeSlider.value = GameSettings.instance.masterVolume;
            masterVolumeSlider.RegisterValueChangedCallback(v =>GameSettings.instance.ChangeSettingByName("MasterVolume", masterVolumeSlider.value));

            var musicVolumeSlider = _audioOptionsPanel.Q<Slider>("MusicVolumeSlider");
            musicVolumeSlider.value = GameSettings.instance.musicVolume;
            musicVolumeSlider.RegisterValueChangedCallback(v => GameSettings.instance.ChangeSettingByName("MusicVolume", musicVolumeSlider.value));

            var sfxVolumeSlider = _audioOptionsPanel.Q<Slider>("SFXVolumeSlider");
            sfxVolumeSlider.value = GameSettings.instance.sfxVolume;
            sfxVolumeSlider.RegisterValueChangedCallback(v => GameSettings.instance.ChangeSettingByName("SFXVolume", sfxVolumeSlider.value));


            _displayButton = _document.rootVisualElement.Q<Button>("Display");
            _displayButton.clicked += _displayButton_clicked;
            _displayOptionsPanel = _displayOptions.CloneTree();
       

            _gameplayButton = _document.rootVisualElement.Q<Button>("Gameplay");
            _gameplayButton.clicked += _gameplayButton_clicked;
            _gameplayOptionsPanel = _gameplayOptions.CloneTree();
            var cameraLookXSlider = _gameplayOptionsPanel.Q<Slider>("HorizontalLook");
            cameraLookXSlider.value = GameSettings.instance.horizontalLookSensitivity;
            cameraLookXSlider.RegisterValueChangedCallback(v=> GameSettings.instance.ChangeSettingByName("HorizontalLookSensitivity", cameraLookXSlider.value));
            var cameraLookYSlider = _gameplayOptionsPanel.Q<Slider>("VerticalLook");
            cameraLookYSlider.value = GameSettings.instance.verticalLookSensitivity;
            cameraLookYSlider.RegisterValueChangedCallback(v => GameSettings.instance.ChangeSettingByName("VerticalLookSensitivity", cameraLookYSlider.value));


            _main.style.display = DisplayStyle.None;

            startMenu = FindObjectOfType<StartMenu>();
            pauseMenu = FindObjectOfType<PauseMenu>();
        }
        public void OpenDisplay()
        {
            startMenu?.CloseDisplay();
            pauseMenu?.CloseDisplay();
            _main.style.display = DisplayStyle.Flex;
        }
        public void CloseDisplay()
        {
            startMenu?.OpenDisplay();
            pauseMenu?.OpenDisplay();
            _main.style.display = DisplayStyle.None;
        }
        private void _gameplayButton_clicked()
        {
            _optionsDisplayPanel.Clear();
            _optionsDisplayPanel.Add(_gameplayOptionsPanel);
            _currentOpenPanel = CurrentOpenPanel.gameplay;
        }

        private void _displayButton_clicked()
        {
            _optionsDisplayPanel.Clear();
            _optionsDisplayPanel.Add(_displayOptionsPanel);
            _currentOpenPanel = CurrentOpenPanel.display;
        }

        private void _audioButton_clicked()
        {
            _optionsDisplayPanel.Clear();
            _optionsDisplayPanel.Add(_audioOptionsPanel);
            _currentOpenPanel = CurrentOpenPanel.audio;
        }

        private void _backButton_clicked()
        {
            CloseDisplay();
        }
    }

}
