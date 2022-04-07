using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;

    private int _currentLevel;
    private int _passedLevel;

    private void OnEnable()
    {
        _uiManager.PressedRestartButton += RestartLevel;
        _uiManager.PressedNextButton += StartNextLevel;
    }

    private void OnDestroy()
    {
        _uiManager.PressedRestartButton -= RestartLevel;
        _uiManager.PressedNextButton -= StartNextLevel;
    }

    private void Start()
    {
        _currentLevel = 0;
    }

    private void StartLevel(int level)
    {
        SceneManager.LoadScene(level);
    }

    private void StartNextLevel()
    {
        _currentLevel++;
        if (_currentLevel >= SceneManager.sceneCountInBuildSettings)
        {
            _currentLevel = Random.Range(0, SceneManager.sceneCountInBuildSettings);
        }
        StartLevel(0);
    }

    private void RestartLevel()
    {
        StartLevel(_currentLevel);
    }
}
