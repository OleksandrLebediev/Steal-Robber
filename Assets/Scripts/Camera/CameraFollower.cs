using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    private Vector3 _startPosition;
    private Vector3 _velocity;

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void LateUpdate()
    {
        if (_target != null)
        {
            Vector3 target = new Vector3(_target.position.x, _startPosition.y, _startPosition.z + _target.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, target, ref _velocity, 0.01f, 40);
        }
    }
}