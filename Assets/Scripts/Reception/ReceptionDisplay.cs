using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReceptionDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _amountCollectObjecView;

    private int _amountObjectsToAccept;

    public void Initialize(int amountObjectsToAccept)
    {
        _amountObjectsToAccept = amountObjectsToAccept;
        UpdateAmountCollectObject(0);
    }

    public void UpdateAmountCollectObject(int amount)
    {
        _amountCollectObjecView.text = $"{amount}/{_amountObjectsToAccept}";
    }
}
