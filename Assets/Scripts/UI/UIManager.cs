using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour, IUIAnswer
{
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private LoseScreen _loseScreen;
    [SerializeField] private WinScreen _winScreen;
    [SerializeField] private TransitionScreen _transitionScreen;

    private IRequestsReporter _requestsReporter;
    private IPlayerEvents _playerEvents;
    private UIInputData _uIInputData;

    public event UnityAction PressedRestartButton;
    public event UnityAction PressedNextButton;

    public void Initialize(IRequestsReporter requestsReporter, 
        IPlayerEvents playerEvents, UIInputData uIInputData, PlayerWallet playerWallet)
    {
        _requestsReporter = requestsReporter;
        _playerEvents = playerEvents;
        _uIInputData = uIInputData;

        _transitionScreen.Show();
        _mainMenu.Initializer(playerWallet, uIInputData);
        _winScreen.Initialize();
        _loseScreen.Initialize();

        _loseScreen.Hide();
        _winScreen.Hide();

        Subscribe();
    }

    private void OnPressedRestartButton()
    {
        _loseScreen.Hide();
        _transitionScreen.Hide();
        PressedRestartButton?.Invoke();
    }

    private void OnPressedNextButton()
    {
        PressedNextButton?.Invoke();
    }

    public void OnPlayerDead()
    {
        _loseScreen.Show(_uIInputData.LevelsInformant.CurrentLevel + 1,
            _uIInputData.BalanceInformant.AmountMoneyPerLevel);
    }

    private void OnAllRequestsCompleted()
    {
        _winScreen.Show(_uIInputData.LevelsInformant.CurrentLevel + 1, 
            _uIInputData.BalanceInformant.AmountMoneyPerLevel);
    }

    private void Subscribe()
    {
        _loseScreen.PressedRestartButton += OnPressedRestartButton;
        _winScreen.PressedNextButton += OnPressedNextButton;
        _requestsReporter.AllRequestsCompleted += OnAllRequestsCompleted;
        _playerEvents.Dead += OnPlayerDead;
    }

    private void Unsubscribe()
    {
        _loseScreen.PressedRestartButton -= OnPressedRestartButton;
        _winScreen.PressedNextButton -= OnPressedNextButton;
        _requestsReporter.AllRequestsCompleted -= OnAllRequestsCompleted;
        _playerEvents.Dead -= OnPlayerDead;
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }
}
