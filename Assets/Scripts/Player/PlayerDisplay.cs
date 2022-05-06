using System;
using UnityEngine;
using TMPro;
public class PlayerDisplay : MonoBehaviour
{
    [SerializeField] private HealthBarDisplay _healthBarDisplay;
    [SerializeField] private CapacityBagDisplay _capacityBagDisplay;

    public void Initialize(int capacity)
    {
        _capacityBagDisplay.SetCapacity(capacity);
        _capacityBagDisplay.Show();
    }
    
    public void ShowHealth()
    {
        _healthBarDisplay.Show();
        _capacityBagDisplay.Hide();
    }

    public void ShowBagCapacity()
    {
        _capacityBagDisplay.Show();
        _healthBarDisplay.Hide();
    }

    public void UpdateHealth(float currenHealth)
    {
        _healthBarDisplay.UpdateUIBar(currenHealth);
    }

    public void UpdateBag(int amount)
    {
        _capacityBagDisplay.UpdateAmount(amount);
    }
}
