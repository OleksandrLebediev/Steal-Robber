using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private LevelsSwitcher _levelsSwitcher;
    [SerializeField] private UIManager _uIManager;
    [SerializeField] private RequestsHandler _requestsHandler;
    [SerializeField] private EnemiesProvider _enemiesProvider;
    [SerializeField] private PlayerProvider _playerProvider;
    [SerializeField] private CameraFollower _cameraFollower;
    [SerializeField] private HostageProvider _hostageProvider;
    [SerializeField] private Joystick _joystick;

    private BinarySaveSystem _saveSystem;

    private void Start()
    {
        _playerProvider.Initialize(_joystick);
        _cameraFollower.Initialize(_playerProvider.Player);
        _requestsHandler.Initialize();
        _enemiesProvider.Initialize();

        if (_hostageProvider != null)
            _hostageProvider.Initialize();

        _saveSystem = new BinarySaveSystem();
        SaveData data = _saveSystem.Load();
        _levelsSwitcher.Initialize(_uIManager, data.Level);
        
        UIInputData uIInputData = new UIInputData(_levelsSwitcher, _playerProvider.Player.Wallet, _joystick);
        _uIManager.Initialize(_requestsHandler, _playerProvider.Player,
            uIInputData, _playerProvider.Player.Wallet);

        _playerProvider.Player.Wallet.Initialize(data.Money);

        _levelsSwitcher.LevelStart += OnLevelStart;
    }

    private void OnLevelStart()
    {
        SaveData data = new SaveData(_levelsSwitcher.CurrentLevel, _playerProvider.Player.Wallet.AmountMoney);
        _saveSystem.Save(data);
    }

    private void OnDestroy()
    {
        _levelsSwitcher.LevelStart -= OnLevelStart;
    }

}
