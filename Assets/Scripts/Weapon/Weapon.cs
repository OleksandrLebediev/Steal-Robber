using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int _damage = 10;
    [SerializeField] private GameObject _muzzlePrefab;
    [SerializeField] private GameObject _muzzlePosition;
    [SerializeField] private AudioClip _gunShotClip;
   
    private EnemiesAudioSourse _source;  
    private Vector2 _audioPitch = new Vector2(.9f, 1.1f);

    public void Initialize(EnemiesAudioSourse enemiesAudioSourse)
    {
        _source = enemiesAudioSourse;
    }

    public void Fire(ITarget target)
    {
        Instantiate(_muzzlePrefab, _muzzlePosition.transform.position,  Quaternion.Euler(0,-90, 0), transform);
        _source.PlayShootAudio(_gunShotClip, Random.Range(_audioPitch.x, _audioPitch.y));
        target.TakeDamage(_damage);
    }
}
