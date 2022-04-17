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

    private void Start()
    {
        _playerPlace.Initialize();
        _cameraFollower.Initialize(_playerPlace.Player);
        _levelsSwitcher.Initialize(_uIManager);
        _requestsHandler.Initialize();
        _enemiesPlace.Initialize();

        UIInputData uIInputData = new UIInputData(_levelsSwitcher, _playerPlace.Player.BalanceInformant);
        _uIManager.Initialize(_requestsHandler, _playerPlace.Player, uIInputData);
    }
}
