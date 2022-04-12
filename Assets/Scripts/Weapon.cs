using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int _damage = 10;
    [SerializeField] private GameObject _muzzlePrefab;
    [SerializeField] private GameObject _muzzlePosition;
    [SerializeField] private AudioClip _gunShotClip;
   
    private AudioSource _source;

    public void Initialize(AudioSource _audioSource)
    {
        _source = _audioSource;
    }

    public void Fire(ITarget target)
    {
        var flash = Instantiate(_muzzlePrefab, _muzzlePosition.transform);
        _source.PlayOneShot(_gunShotClip);
        target.TakeDamage(_damage);
    }
}
