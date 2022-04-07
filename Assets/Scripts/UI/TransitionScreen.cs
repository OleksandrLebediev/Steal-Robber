using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionScreen : MonoBehaviour
{
    [SerializeField] private Image _imageTransition;

    public void Show()
    {
        gameObject.SetActive(true);
        LeanTween.alpha(_imageTransition.rectTransform, 0, 1).setOnComplete(() => { gameObject.SetActive(false); });
    }

    public void Hide()
    {
        gameObject.SetActive(true);
        _imageTransition.color = Color.black;
    }
}
