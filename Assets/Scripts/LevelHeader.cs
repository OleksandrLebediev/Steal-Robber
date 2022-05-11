using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelHeader : MonoBehaviour
{
    private TMP_Text _header;

    private void Awake()
    {
        _header = GetComponent<TMP_Text>();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
    
    public void Initialize(ILevelsInformant levelsInformant)
    {
        _header.text =  $"Level {levelsInformant.CurrentLevel + 1}";
    }
}
