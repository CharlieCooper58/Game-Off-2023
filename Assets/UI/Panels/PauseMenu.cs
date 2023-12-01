using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using FMODUnity;

namespace UI.Panels
{
    public class PauseMenu : MonoBehaviour
    {

        private Button _resume;
        private Button _options;
        private Button _quit;

        private UIDocument _document;
        private VisualElement _main;

        [SerializeField] private SettingsMenu _settingsMenu;

        [SerializeField] EventReference buttonClickSound;

        public void Awake()
        {
            _document = GetComponent<UIDocument>();
            _main = _document.rootVisualElement.Q<VisualElement>("Main");

            _resume = _document.rootVisualElement.Q<Button>("Resume");
            _options = _document.rootVisualElement.Q<Button>("Options");
            _quit = _document.rootVisualElement.Q<Button>("Quit");
            
            _resume.clicked += ResumeGame;
            _options.clicked += OpenOptions;
            _quit.clicked += QuitGame;


            _main.style.display = DisplayStyle.None;
        }

        public void OpenDisplay()
        {
            _main.style.display = DisplayStyle.Flex;
        }
        public void CloseDisplay()
        {
            _main.style.display = DisplayStyle.None;
        }

        public void TogglePauseState()
        {
            if (Time.timeScale == 0)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        private void ResumeGame()
        {
            AudioManager.instance.Play(buttonClickSound);
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;

            Time.timeScale = 1;
            _settingsMenu.CloseDisplay();
            _main.style.display = DisplayStyle.None;
        }

        private void PauseGame()
        {
            AudioManager.instance.Play(buttonClickSound);
            UnityEngine.Cursor.lockState = CursorLockMode.Confined;
            UnityEngine.Cursor.visible = true;
            Time.timeScale = 0;
            _main.style.display = DisplayStyle.Flex;
        }
        
        private void OpenOptions()
        {
            _settingsMenu.OpenDisplay();
        }
        
        private void QuitGame()
        {
            AudioManager.instance.Play(buttonClickSound);
            ResumeGame();
            SceneManager.LoadScene("StartMenu");
        }
    }
}
