using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EnemiesAudioSourse : MonoBehaviour
{
    [SerializeField] private AudioClip _audioCollected;

    private AudioSource _audioSource;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayCollectedAudio()
    {
        _audioSource.PlayOneShot(_audioCollected);
    }

    public void PlayShootAudio(AudioClip clip, float pitch)
    {
        _audioSource.pitch = pitch;
        _audioSource.PlayOneShot(clip);
        _audioSource.pitch = 1;
    }
}
