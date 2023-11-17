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
        [SerializeField] private VisualTreeAsset _displayOptions;
        private VisualElement _displayOptionsPanel;
        [SerializeField] private VisualTreeAsset _gameplayOptions;
        private VisualElement _gameplayOptionsPanel;


        private VisualElement _main;
        private Button _backButton;
        private Button _audioButton;
        private Button _displayButton;
        private Button _gameplayButton;

        private VisualElement _optionsDisplayPanel;

        private UIDocument _document;

        StartMenu startMenu;
        PauseMenu pauseMenu;
        private void Awake()
        {
            _document = GetComponent<UIDocument>();
            _main = _document.rootVisualElement.Q<VisualElement>("Main");
            _optionsDisplayPanel = _document.rootVisualElement.Q<VisualElement>("OptionsSpace");

            _backButton = _document.rootVisualElement.Q<Button>("BackButton");
            _backButton.clicked += _backButton_clicked;

            _audioButton = _document.rootVisualElement.Q<Button>("Audio");
            _audioButton.clicked += _audioButton_clicked;
            _audioOptionsPanel = _audioOptions.CloneTree();
            var masterVolumeSlider = _audioOptionsPanel.Q<Slider>("MasterVolumeSlider");
            var musicVolumeSlider = _audioOptionsPanel.Q<Slider>("MusicVolumeSlider");
            var sfxVolumeSlider = _audioOptionsPanel.Q<Slider>("SFXVolumeSlider");


            _displayButton = _document.rootVisualElement.Q<Button>("Display");
            _displayButton.clicked += _displayButton_clicked;
            _displayOptionsPanel = _displayOptions.CloneTree();
       

            _gameplayButton = _document.rootVisualElement.Q<Button>("Gameplay");
            _gameplayButton.clicked += _gameplayButton_clicked;
            _gameplayOptionsPanel = _gameplayOptions.CloneTree();
            var cameraLookXSlider = _gameplayOptionsPanel.Q<Slider>("horizontalLookSlider");
            var cameraLookYSlider = _gameplayOptionsPanel.Q<Slider>("verticalLookSlider");

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
        }

        private void _displayButton_clicked()
        {
            _optionsDisplayPanel.Clear();
            _optionsDisplayPanel.Add(_displayOptionsPanel);
        }

        private void _audioButton_clicked()
        {
            _optionsDisplayPanel.Clear();
            _optionsDisplayPanel.Add(_audioOptionsPanel);
        }

        private void _backButton_clicked()
        {
            CloseDisplay();
        }
    }

}
