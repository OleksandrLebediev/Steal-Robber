using DG.Tweening;
using TMPro;
using UnityEngine;

namespace LoseScreenElement
{
    public class LevelFailedTitle : MonoBehaviour
    {
        private TMP_Text _title;
        private float _durationShowAnimation;

        private void Awake()
        {
            _title = GetComponent<TMP_Text>();
        }

        public void Initialize(float durationShowAnimation)
        {
            _durationShowAnimation = durationShowAnimation;
        }

        public void Show(int numberfailedLevel)
        {
            gameObject.SetActive(true);
            ShowAnimations();
            _title.text = $"level {numberfailedLevel}\nfailed";
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void ShowAnimations()
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(1, _durationShowAnimation).SetEase(Ease.OutBack);
        }
    }
}