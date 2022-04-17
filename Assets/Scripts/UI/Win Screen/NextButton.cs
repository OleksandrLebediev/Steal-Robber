using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace WinScreenElement
{
    public class NextButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        private float _durationShowAnimation;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        public void Initialize(float durationShowAnimation)
        {
            _durationShowAnimation = durationShowAnimation;
        }

        public void Show()
        {
            gameObject.SetActive(true);
            ShowAnimations();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Subscribe(UnityAction call)
        {
            _button.onClick.AddListener(call);
        }

        public void Unsubscribe(UnityAction call)
        {
            _button.onClick.RemoveListener(call);
        }

        private void ShowAnimations()
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(1, _durationShowAnimation).SetEase(Ease.OutBack);
        }
    }
}

