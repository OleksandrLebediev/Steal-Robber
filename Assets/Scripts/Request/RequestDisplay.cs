using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RequestDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _numberOfTargetsView;
    [SerializeField] private Image _targetIcon;

    public void Initialize(int numberOfTargets, Sprite spriteOfTarget)
    {
        _targetIcon.sprite = spriteOfTarget;
        UpdateAmountCollectObject(numberOfTargets);
    }

    public void UpdateAmountCollectObject(int amount)
    {
        _numberOfTargetsView.text = $"{amount}";
    }
}