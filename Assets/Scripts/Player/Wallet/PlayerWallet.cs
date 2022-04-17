using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerWallet : MonoBehaviour, IAcceptingMoney, IBalanceInformant
{
    public event UnityAction<int> AmountMoneyChanged;
    
    private int _amountMoney;
    private int _moneyPerLevel;

    public int AmountMoney => _amountMoney;
    public int AmountMoneyPerLevel => _moneyPerLevel;

    public void Initialize(int amount)
    {
        _amountMoney = amount;
        AmountMoneyChanged?.Invoke(amount);
    }

    public void AddMoney(int amount)
    {
        _amountMoney += amount;
        _moneyPerLevel += amount;
        AmountMoneyChanged?.Invoke(_amountMoney);
    }

    public void WithdrawMoney(int amount)
    {
        _amountMoney -= amount;
        AmountMoneyChanged?.Invoke(_amountMoney);
    }

}
