using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace UI.Panels
{
    public class PauseMenu : MonoBehaviour
    {
        private Button _resume;
        private Button _options;
        private Button _quit;

        private UIDocument _document;
        private VisualElement _main;

        public void OnEnable()
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
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;

            Time.timeScale = 1;
            _main.style.display = DisplayStyle.None;
        }

        private void PauseGame()
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Confined;
            UnityEngine.Cursor.visible = true;
            Time.timeScale = 0;
            _main.style.display = DisplayStyle.Flex;
        }
        
        private void OpenOptions()
        {
            throw new System.NotImplementedException();
        }
        
        private void QuitGame()
        {
            ResumeGame();
            SceneManager.LoadScene("StartMenu");
        }
    }
}
