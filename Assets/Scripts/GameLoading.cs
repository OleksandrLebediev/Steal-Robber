using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoading : MonoBehaviour
{
    private BinarySaveSystem _saveSystem;

    private void Start()
    {
        _saveSystem = new BinarySaveSystem();
        SaveData saveData = _saveSystem.Load();
        SceneManager.LoadScene("Level_" + saveData.Level);
    }
}
