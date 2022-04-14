using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speedMove;
    [SerializeField] private float _speedRotation;

    private CharacterController _characterController;
    private float _ground = 0.175000f;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    public void Move(Vector3 direction)
    {
        transform.rotation = Rotation(direction);
        _characterController.Move(transform.forward * _speedMove * Time.deltaTime);
    }

    private Quaternion Rotation(Vector3 direction)
    {
        Vector3 direct = Vector3.RotateTowards(transform.forward, direction, _speedRotation, 0.0f);
        return Quaternion.LookRotation(direct);
    }
}