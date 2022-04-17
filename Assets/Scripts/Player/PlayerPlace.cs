using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlace : MonoBehaviour
{
    private Player _player;
    public Player Player => _player;    

    private void Awake()
    {
        _player = GetComponentInChildren<Player>();
    }

    public void Initialize()
    {
        _player.Initialize();
    }
}
