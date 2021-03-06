using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelsSwitcher : MonoBehaviour, ILevelsInformant
{
    private IUIAnswer _uIAnswer;
    private int _currentLevel;

    private readonly string levelSceneName = "Level_";

    public int CurrentLevel => _currentLevel;

    public event UnityAction LevelChanges;
    public event UnityAction LevelRestarted;
    public event UnityAction LevelStart;

    private void OnDestroy()
    {
        _uIAnswer.PressedRestartButton -= RestartLevel;
        _uIAnswer.PressedNextButton -= StartNextLevel;
    }

    public void Initialize(IUIAnswer uIAnswer, int currentLevel)
    {
        _currentLevel = currentLevel;
        _uIAnswer = uIAnswer;
        _uIAnswer.PressedRestartButton += RestartLevel;
        _uIAnswer.PressedNextButton += StartNextLevel;
    }

    private void StartLevel(int level)
    {
        LevelStart?.Invoke();
        SceneManager.LoadScene(levelSceneName + level);
    }

    private void StartNextLevel()
    {
        LevelChanges?.Invoke();
        _currentLevel++;
        if (_currentLevel >= SceneManager.sceneCountInBuildSettings - 1)
        {
            _currentLevel = Random.Range(0, SceneManager.sceneCountInBuildSettings - 1);
        }
        StartLevel(_currentLevel);
    }

    private void RestartLevel()
    {
        LevelRestarted?.Invoke();
        StartLevel(_currentLevel);
    }
}
