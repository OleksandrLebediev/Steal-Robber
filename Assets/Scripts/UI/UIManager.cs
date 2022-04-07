using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    [SerializeField] private LossScreen _lossScreen;
    [SerializeField] private WinScreen _winScreen;
    [SerializeField] private TransitionScreen _transitionScreen;
    [SerializeField] private CheckingRequests _checkingRequests;

    public event UnityAction PressedRestartButton;
    public event UnityAction PressedNextButton;

    private void Start()
    {
        _transitionScreen.Show();
        _lossScreen.Hide();
        _winScreen.Hide();
    }

    private void OnEnable()
    {
        _lossScreen.PressedRestartButton += OnPressedRestartButton;
        _winScreen.PressedNextButton += OnPressedNextButton;
        _checkingRequests.AllRequestsCompleted += OnAllRequestsCompleted;
        GlobalEventManager.PlayerDead += OnPlayerDead;
    }


    private void OnDestroy()
    {
        _lossScreen.PressedRestartButton -= OnPressedRestartButton;
        _winScreen.PressedNextButton -= OnPressedNextButton;
        _checkingRequests.AllRequestsCompleted -= OnAllRequestsCompleted;
        GlobalEventManager.PlayerDead -= OnPlayerDead;
    }

    private void OnPressedRestartButton()
    {
        _lossScreen.Hide();
        _transitionScreen.Hide();
        PressedRestartButton?.Invoke();
    }

    private void OnPressedNextButton()
    {
        PressedNextButton?.Invoke();
    }

    public void OnPlayerDead()
    {
        _lossScreen.Show();
    }

    private void OnAllRequestsCompleted()
    {
        _winScreen.Show();
    }
}
