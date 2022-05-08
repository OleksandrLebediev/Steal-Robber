using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int _damage = 10;
    [SerializeField] private GameObject _muzzlePrefab;
    [SerializeField] private GameObject _muzzlePosition;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private AudioClip _gunShotClip;
   
    private EnemiesAudioSourse _source;

    public void Initialize(EnemiesAudioSourse enemiesAudioSourse)
    {
        _source = enemiesAudioSourse;
    }

    public void Fire(ITarget target)
    {
        var flash = Instantiate(_muzzlePrefab, _muzzlePosition.transform);
        _source.PlayShootAudio(_gunShotClip);
        target.TakeDamage(_damage);
    }
}
