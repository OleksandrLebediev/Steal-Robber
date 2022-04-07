using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarDisplay : MonoBehaviour
{
    [SerializeField] private Image _fill;

    public void UpdateUIBar(float currentHealth)
    {
        _fill.fillAmount = currentHealth;
    }
}
