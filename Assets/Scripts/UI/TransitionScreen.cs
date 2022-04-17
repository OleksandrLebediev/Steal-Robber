using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TransitionScreen : MonoBehaviour
{
    [SerializeField] private Image _imageTransition;

    public void Show()
    {
        gameObject.SetActive(true);
        _imageTransition.DOFade(0, 1).OnComplete(() => { gameObject.SetActive(false); });
    }

    public void Hide()
    {
        gameObject.SetActive(true);
        _imageTransition.color = Color.black;
    }
}
