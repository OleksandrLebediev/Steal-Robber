using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollower : MonoBehaviour
{
    private ILocation _target;
    private Vector3 _startPosition;
    private Vector3 _velocity;

    public void Initialize(ILocation target)
    {
        _startPosition = transform.position;
        _target = target;
    }

    private void LateUpdate()
    {
        if (_target != null)
        {
           Vector3 target = new Vector3(_target.CurrentTransform.position.x, _startPosition.y, _startPosition.z + _target.CurrentTransform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, target, ref _velocity, 0.01f, 40);
        }
    }
}
