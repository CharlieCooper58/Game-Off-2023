using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace UI.Panels
{
    public class StartMenu : MonoBehaviour
    {

        private TextElement _titleText;
        private Button _play;
        private Button _options;
        private Button _quit;
        private VisualElement _main;

        [SerializeField] private SettingsMenu _settingsMenu;


        private UIDocument _document;

        public void OnEnable()
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Confined;
            UnityEngine.Cursor.visible = true;
            
            _document = GetComponent<UIDocument>();

            _main = _document.rootVisualElement.Q<VisualElement>("Main");

            _titleText = _document.rootVisualElement.Q<TextElement>("Title");

            _play = _document.rootVisualElement.Q<Button>("Play");
            _options = _document.rootVisualElement.Q<Button>("Options");
            _quit = _document.rootVisualElement.Q<Button>("Quit");
            
            _play.clicked += PlayGame;
            _options.clicked += OpenOptions;
            _quit.clicked += QuitGame;

        }
        public void OpenDisplay()
        {
            _main.style.display = DisplayStyle.Flex;
        }
        public void CloseDisplay()
        {
            _main.style.display = DisplayStyle.None;
        }
        private static void PlayGame()
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;

            SceneManager.LoadScene("Tutorial-Map 1");
        }
        
        private void OpenOptions()
        {
            _settingsMenu.OpenDisplay();
        }
        private static void QuitGame()
        {
            Application.Quit(1);
        }
    }
}
