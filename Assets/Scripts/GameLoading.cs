using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLoading : MonoBehaviour
{
    [SerializeField] private Image _loadSlider;
    private BinarySaveSystem _saveSystem;

    private void Start()
    {
        _saveSystem = new BinarySaveSystem();
        SaveData saveData = _saveSystem.Load();
        string level = "Level_" + saveData.Level;
        StartCoroutine(LoadAsync(level));
    }

    private IEnumerator LoadAsync(string level)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(level);
        while (operation.isDone == false)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            _loadSlider.fillAmount = progress;
            yield return null;
        }
    }
}
