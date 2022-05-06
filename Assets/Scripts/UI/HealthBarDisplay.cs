using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarDisplay : MonoBehaviour
{
    [SerializeField] private Image _fill;

    private Coroutine _waitHideCoroutine;
    private bool _counterHideActive;
    private float _delayHide = 2f;

    public void Show()
    {
        gameObject.SetActive(true);
        if (_counterHideActive == true) 
            StopCoroutine(_waitHideCoroutine);
        _waitHideCoroutine = StartCoroutine(WaitHideCoroutine());     
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void UpdateUIBar(float currentHealth)
    {
        _fill.fillAmount = currentHealth;
    }

    private IEnumerator WaitHideCoroutine()
    {
        _counterHideActive = true;

        yield return new WaitForSeconds(_delayHide);
        gameObject.SetActive(false);
        _counterHideActive = false;
    }
}
