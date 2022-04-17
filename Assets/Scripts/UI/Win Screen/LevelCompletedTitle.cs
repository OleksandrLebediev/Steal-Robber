using UnityEngine;
using TMPro;
using DG.Tweening;

namespace WinScreenElement
{
    public class LevelCompletedTitle : MonoBehaviour
    {
        private TMP_Text _title;
        private float _durationShowAnimation;

        private void Awake()
        {
            _title = GetComponent<TMP_Text>();
        }

        public void Initialize( float durationShowAnimation)
        {
            _durationShowAnimation = durationShowAnimation;
        }

        public void Show(int numberCompletedLevel)
        {
            gameObject.SetActive(true);
            ShowAnimations();
            _title.text = $"level {numberCompletedLevel}\ncompleted";
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

