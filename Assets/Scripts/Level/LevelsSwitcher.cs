using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsSwitcher : MonoBehaviour, ILevelsInformant
{
    private IUIAnswer _uIAnswer;
    private int _currentLevel;

    public int CurrentLevel => _currentLevel;

    private void OnDestroy()
    {
        _uIAnswer.PressedRestartButton -= RestartLevel;
        _uIAnswer.PressedNextButton -= StartNextLevel;
    }

    public void Initialize(IUIAnswer uIAnswer)
    {
        _currentLevel = 0;
        _uIAnswer = uIAnswer;
        _uIAnswer.PressedRestartButton += RestartLevel;
        _uIAnswer.PressedNextButton += StartNextLevel;
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
