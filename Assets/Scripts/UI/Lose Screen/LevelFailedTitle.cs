using DG.Tweening;
using TMPro;
using UnityEngine;

namespace LoseScreenElement
{
    public class LevelFailedTitle : MonoBehaviour
    {
        [SerializeField] private TMP_Text _titleLevel;
        [SerializeField] private TMP_Text _titleFailed;
        private float _durationShowAnimation;
        
        public void Initialize(float durationShowAnimation)
        {
            _durationShowAnimation = durationShowAnimation;
        }

        public void Show(int numberfailedLevel)
        {
            gameObject.SetActive(true);
            ShowAnimations();
            _titleLevel.text = $"level {numberfailedLevel}";
            _titleFailed.text = "Failed";
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void ShowAnimations()
        {
            _titleLevel.transform.localScale = Vector3.zero;
            _titleFailed.transform.localScale = Vector3.zero;
            _titleLevel.transform.DOScale(1, _durationShowAnimation).SetEase(Ease.OutBack).OnComplete(
                () => { _titleFailed.transform.DOScale(1, _durationShowAnimation).SetEase(Ease.OutBack); }
            );
        }
    }
}