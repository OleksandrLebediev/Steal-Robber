using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class HostageProvider : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private Hostage[] _hostages;

    private void Awake()
    {
        _hostages = GetComponentsInChildren<Hostage>();
    }

    public void Initialize()
    {
        foreach (var hostage in _hostages)
        {
            hostage.Initialize(_audioSource);
        }
    }
}
