using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    [SerializeReference] private Button _nextButton;

    public event UnityAction PressedNextButton;

    private void OnEnable()
    {
        _nextButton.onClick.AddListener(OnPressedRestartButton);
    }

    private void OnDestroy()
    {
        _nextButton.onClick.RemoveListener(OnPressedRestartButton);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnPressedRestartButton()
    {
        PressedNextButton?.Invoke();
    }
}
