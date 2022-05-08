using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemiesAudioSourse))]
public class EnemiesProvider : MonoBehaviour
{
    private Enemy[] _enemies;
    private EnemiesAudioSourse _enemiesAudioSourse;

    private void Awake()
    {
        _enemies = GetComponentsInChildren<Enemy>();
        _enemiesAudioSourse = GetComponent<EnemiesAudioSourse>();
    }

    public void Initialize()
    {
        foreach (Enemy enemy in _enemies)
        {
            enemy.Initialize(_enemiesAudioSourse);
        }
    }
}
