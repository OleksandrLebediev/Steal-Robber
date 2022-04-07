using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Enemy[] _enemies;

    public int CountEnemies => _enemies.Length;
}
