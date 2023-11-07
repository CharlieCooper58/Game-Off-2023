using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace UI.Panels
{
    public class StartMenu : MonoBehaviour
    {
        private Button _play;
        private Button _options;
        private Button _quit;

        private UIDocument _document;

        public void OnEnable()
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Confined;
            UnityEngine.Cursor.visible = true;
            
            _document = GetComponent<UIDocument>();
            
            _play = _document.rootVisualElement.Q<Button>("Play");
            _options = _document.rootVisualElement.Q<Button>("Options");
            _quit = _document.rootVisualElement.Q<Button>("Quit");
            
            _play.clicked += PlayGame;
            _options.clicked += OpenOptions;
            _quit.clicked += QuitGame;
        }

        private static void PlayGame()
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;

            SceneManager.LoadScene("Gameplay Testing");
        }
        
        private static void OpenOptions()
        {
            throw new System.NotImplementedException();
        }
        
        private static void QuitGame()
        {
            Application.Quit(1);
        }
    }
}
