using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerWallet : MonoBehaviour, IAcceptingMoney
{
    public event UnityAction<int> AmountMoneyChanged;
    
    private int _amountMoney;

    public void AddMoney(int amount)
    {
        _amountMoney += amount;
        AmountMoneyChanged?.Invoke(_amountMoney);
    }

    public void WithdrawMoney(int amount)
    {
        _amountMoney -= amount;
        AmountMoneyChanged?.Invoke(_amountMoney);
    }

}
