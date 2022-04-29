using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesProvider : MonoBehaviour
{
    private Enemy[] _enemies;

    private void Awake()
    {
        _enemies = GetComponentsInChildren<Enemy>();
    }

    public void Initialize()
    {
        foreach (Enemy enemy in _enemies)
        {
            enemy.Initialize();
        }
    }
}
