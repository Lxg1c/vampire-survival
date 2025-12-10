using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class PauseManagerInputSystem : MonoBehaviour
    {
        public static PauseManagerInputSystem Instance { get; private set; }
    
        [SerializeField] private GameObject pauseMenuUI;
        private PlayerInputActions _playerInput;
        private bool _isPaused;
    
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
    
            _playerInput = new PlayerInputActions();
            _playerInput.UI.Pause.performed += _ => TogglePause();
        }
    
        private void OnEnable()
        {
            _playerInput?.Enable();
        }
    
        private void OnDisable()
        {
            _playerInput?.Disable();
        }
    
        private void OnDestroy()
        {
            if (_playerInput != null)
            {
                _playerInput.UI.Pause.performed -= _ => TogglePause();
                _playerInput.Dispose();
            }
        }
    
        private void TogglePause()
        {
            if (_isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    
        private void PauseGame()
        {
            _isPaused = true;
            Time.timeScale = 0f;
            pauseMenuUI?.SetActive(true);
            
            _playerInput.Player.Disable();
            
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            AudioListener.pause = true;
        }
    
        private void ResumeGame()
        {
            _isPaused = false;
            Time.timeScale = 1f;
            pauseMenuUI?.SetActive(false);
            
            _playerInput.Player.Enable();
            
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            AudioListener.pause = false;
        }
    
        public void OnResumeButton()
        {
            ResumeGame();
        }
    
        public void OnExitButton()
        {
            // —брасываем таймскейл перед загрузкой сцены
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
        }
    
        public void OnRestartButton()
        {
            // —брасываем таймскейл перед загрузкой сцены
            Time.timeScale = 1f;
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
    }
}