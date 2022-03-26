using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour
{
    private Character _player;
    private Vector3 _startPosition;
    private float _speed = 1;

    private void Start()
    {
        _startPosition = transform.position;
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void Initialize(Character player)
    {
        _player = player;
    }

    private void LateUpdate()
    {
        if(_player != null)
        {
            Vector3 target = new Vector3(_player.transform.position.x, _startPosition.y, _startPosition.z + _player.transform.position.z);
            transform.position = Vector3.Lerp(transform.position, target, _speed);
        }
    }
}
