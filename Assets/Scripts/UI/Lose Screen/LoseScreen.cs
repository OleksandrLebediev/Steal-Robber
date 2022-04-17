using CommonUIElement;
using LoseScreenElement;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class LoseScreen : MonoBehaviour
{
    [SerializeField] private LevelFailedTitle _levelFailedTitle;
    [SerializeField] private RewardDisplay _rewardDisplay;
    [SerializeField] private RestartButton _restartButton;

    private readonly float _delayBetweenShowed = 0.5f;
    private readonly float _durationShowAnimation = 0.5f;
    private readonly float _durationIncrementAnimation = 1;

    public event UnityAction PressedRestartButton;

    public void Initialize()
    {
        _levelFailedTitle.Initialize(_durationShowAnimation);
        _rewardDisplay.Initialize(_durationShowAnimation, _durationIncrementAnimation);
        _restartButton.Initialize(_durationShowAnimation);

        _restartButton.Subscribe(OnPressedRestartButton);
        _levelFailedTitle.Hide();
        _rewardDisplay.Hide();
        _restartButton.Hide();
    }

    private void OnDestroy()
    {
        _restartButton.Unsubscribe(OnPressedRestartButton);
    }

    public void Show(int number—urrentLevel, int amountMoneyPerLevel)
    {
        gameObject.SetActive(true);
        StartCoroutine(ShowScenarioCoroutine(number—urrentLevel, amountMoneyPerLevel));
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnPressedRestartButton()
    {
        PressedRestartButton?.Invoke();
    }

    private IEnumerator ShowScenarioCoroutine(int numberCompletedLevel, int amountMoneyPerLevel)
    {
        yield return new WaitForSeconds(_delayBetweenShowed);
        _levelFailedTitle.Show(numberCompletedLevel);

        yield return new WaitForSeconds(_delayBetweenShowed);
        yield return StartCoroutine(_rewardDisplay.ShowCoroutine(amountMoneyPerLevel));

        _restartButton.Show();
    }
}
