using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RequestDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _numberOfTargetsView;
    [SerializeField] private Image _targetIcon;
    [SerializeField] private Image _completedIcon;

    public void Initialize(int numberOfTargets, Sprite spriteOfTarget)
    {
        _targetIcon.sprite = spriteOfTarget;
        UpdateAmountCollectObject(numberOfTargets);
    }

    public void UpdateAmountCollectObject(int amount)
    {
        _numberOfTargetsView.text = $"{amount}";
    }

    public void SetCompletedDisplay()
    {
        _completedIcon.gameObject.SetActive(true);
        _numberOfTargetsView.gameObject.SetActive(false);
    }
}