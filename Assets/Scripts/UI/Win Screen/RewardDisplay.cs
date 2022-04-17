using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

namespace CommonUIElement
{
    public class RewardDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text _amountMoney;

        private float _durationShowAnimation;
        private float _durationIncrementAnimation;
        private int _profit = 0;

        public void Initialize(float durationShowAnimation, float durationIncrementAnimation)
        {
            _durationShowAnimation = durationShowAnimation;
            _durationIncrementAnimation = durationIncrementAnimation;
        }

        public IEnumerator ShowCoroutine(int profitCompletedLevel)
        {
            gameObject.SetActive(true);
            ShowAnimations();
            yield return new WaitForSeconds(_durationShowAnimation);

            ProfitIncrementAnimation(profitCompletedLevel);
            yield return new WaitForSeconds(_durationIncrementAnimation);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void ProfitIncrementAnimation(int amountMoneyPerLevel)
        {
            DOTween.To(() => _profit, x => _profit = x, amountMoneyPerLevel, _durationIncrementAnimation).OnUpdate(UpdateProfit);
        }

        private void UpdateProfit()
        {
            _amountMoney.text = _profit.ToString();
        }

        private void ShowAnimations()
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(1, _durationShowAnimation).SetEase(Ease.OutBack);
        }
    }
}

