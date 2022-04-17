using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using WinScreenElement;
using CommonUIElement;

public class WinScreen : MonoBehaviour
{
    [SerializeReference] private NextButton _nextButton;
    [SerializeReference] private LevelCompletedTitle _levelCompletedTitle;
    [SerializeReference] private RewardDisplay _rewar‚Display;

    private readonly float _delayBetweenShowed = 0.5f;
    private readonly float _durationShowAnimation = 0.5f;
    private readonly float _durationIncrementAnimation = 1;

    public event UnityAction PressedNextButton;

    public void Initialize()
    {
        _levelCompletedTitle.Initialize(_durationShowAnimation);
        _rewar‚Display.Initialize(_durationShowAnimation, _durationIncrementAnimation);
        _nextButton.Initialize(_durationShowAnimation);

        _nextButton.Subscribe(OnPressedRestartButton);
        _levelCompletedTitle.Hide();
        _rewar‚Display.Hide();
        _nextButton.Hide();
    }

    private void OnDestroy()
    {
        _nextButton.Unsubscribe(OnPressedRestartButton);
    }

    public void Show(int numberCurrentLevel, int amountMoneyPerLevel)
    {
        gameObject.SetActive(true);
        StartCoroutine(ShowScenarioCoroutine(numberCurrentLevel, amountMoneyPerLevel));
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnPressedRestartButton()
    {
        PressedNextButton?.Invoke();
    }

    private IEnumerator ShowScenarioCoroutine(int numberCompletedLevel, int amountMoneyPerLevel)
    {
        yield return new WaitForSeconds(_delayBetweenShowed);
        _levelCompletedTitle.Show(numberCompletedLevel);

        yield return new WaitForSeconds(_delayBetweenShowed);
        yield return StartCoroutine(_rewar‚Display.ShowCoroutine(amountMoneyPerLevel));

        _nextButton.Show();
    }
}
