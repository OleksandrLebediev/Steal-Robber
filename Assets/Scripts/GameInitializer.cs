using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private LevelsSwitcher _levelsSwitcher;
    [SerializeField] private UIManager _uIManager;
    [SerializeField] private RequestsHandler _requestsHandler;
    [SerializeField] private EnemiesPlace _enemiesPlace;
    [SerializeField] private PlayerPlace _playerPlace;
    [SerializeField] private CameraFollower _cameraFollower;
    
    private BinarySaveSystem _saveSystem;

    private void Start()
    {
        _playerPlace.Initialize();
        _cameraFollower.Initialize(_playerPlace.Player);
        _requestsHandler.Initialize();
        _enemiesPlace.Initialize();

        UIInputData uIInputData = new UIInputData(_levelsSwitcher, _playerPlace.Player.Wallet);
        _uIManager.Initialize(_requestsHandler, _playerPlace.Player, uIInputData);

        _saveSystem = new BinarySaveSystem();
        SaveData data = _saveSystem.Load();
        _levelsSwitcher.Initialize(_uIManager, data.Level);
        _playerPlace.Player.Wallet.Initialize(data.Money);
        _levelsSwitcher.LevelChanges += OnLevelChanges;
    }

    private void OnLevelChanges()
    {
        SaveData data = new SaveData(_levelsSwitcher.CurrentLevel, _playerPlace.Player.Wallet.AmountMoney);
        _saveSystem.Save(data);
    }

    private void OnDestroy()
    {
        _levelsSwitcher.LevelChanges -= OnLevelChanges;
    }

    private void OnApplicationQuit()
    {
        SaveData data = new SaveData(_levelsSwitcher.CurrentLevel, _playerPlace.Player.Wallet.AmountMoney);
        _saveSystem.Save(data);
    }


}
