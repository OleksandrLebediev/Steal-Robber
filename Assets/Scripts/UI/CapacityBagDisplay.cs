using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class CapacityBagDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _capacity;
    private int _currentCapacity;
    private Coroutine _waitHideCoroutine;
    private bool _counterHideActive;
    private float _delayHide = 1f;
    
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

    public void UpdateAmount(int amount)
    {
        _capacity.text = $"{amount}/{_currentCapacity}";
        _capacity.color = Color.white;
    }

    public void SetCapacity(int capacity)
    {
        _capacity.text = $"0/{capacity}";
        _currentCapacity = capacity;
    }

    public void ShowFullNotice()
    {
        _capacity.text = "FULL!";
        _capacity.color = Color.red;
    }
    
    private IEnumerator WaitHideCoroutine()
    {
        _counterHideActive = true;

        yield return new WaitForSeconds(_delayHide);
        gameObject.SetActive(false);
        _counterHideActive = false;
    }
}
