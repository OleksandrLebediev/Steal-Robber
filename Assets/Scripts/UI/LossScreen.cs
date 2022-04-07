using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LossScreen : MonoBehaviour
{
    [SerializeReference] private Button _restartButton;

    public event UnityAction PressedRestartButton;

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(OnPressedRestartButton);
    }

    private void OnDestroy()
    {
        _restartButton.onClick.RemoveListener(OnPressedRestartButton);
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
        PressedRestartButton?.Invoke();
    }
}
