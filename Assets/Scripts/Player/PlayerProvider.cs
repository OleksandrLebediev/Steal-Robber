using UnityEngine;

public class PlayerProvider : MonoBehaviour
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
