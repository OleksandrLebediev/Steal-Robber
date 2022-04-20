using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform[] _pathTargetList;
    [SerializeField] private PatroleType _patruleType;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _rotateSpeed;
    public Transform[] PathTargetList => _pathTargetList;

    private NavMeshAgent _agent;
    private CapsuleCollider _capsuleCollider;
    private int _currentPath;

    public PatroleType Patrole => _patruleType;
    public float WalkSpeed => _walkSpeed;
    public float RunSpeed => _runSpeed;


    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }

    public void Initialize()
    {
        TrySetDefaultPath();
    }

    public void SetSpeed(float speed)
    {
        _agent.speed = speed;
    }

    public void ResetAgentPaths()
    {
        if (_agent.isActiveAndEnabled == true)
            _agent.ResetPath();
    }

    public IEnumerator MoveToTargetCoroutine(Vector3 target, float? maxTimeWay = null, float timeWay = 0)
    {
        Vector3 distance;

        while (true)
        {
            _agent.SetDestination(target);
            distance = (target - transform.position);
            distance.y = 0f;
            if (distance.magnitude < 0.01f) yield break;

            if (maxTimeWay == null) yield return null;

            timeWay += Time.deltaTime;
            if (timeWay >= maxTimeWay && _agent.velocity.magnitude == 0) yield break;
            yield return null;
        }
    }

    public IEnumerator RotationCoroutine(Vector3 target)
    {
        while (true)
        {
            Vector3 lookPos = target - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _rotateSpeed * Time.deltaTime);

            if (Quaternion.Angle(transform.rotation, rotation) <= 0.001f)
            {
                break;
            }
            yield return null;
        }
    }

    public IEnumerator RotationCoroutine(Transform target, float speed = 10)
    {
        while (true)
        {
            Vector3 lookPos = target.position - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
            yield return null;
        }
    }

    public IEnumerator RotationAround(Quaternion rotation)
    {
        while (true)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _rotateSpeed * Time.deltaTime);

            if (Quaternion.Angle(transform.rotation, rotation) <= 0.001f) break;
            yield return null;
        }

    }

    public Vector3 GetPath()
    {
        Vector3 path = PathTargetList[_currentPath].position;
        _currentPath += 1;
        _currentPath = _currentPath % PathTargetList.Length;
        return path;
    }

    public Vector3 GetRandomDirection()
    {
        Vector3 center = transform.position;
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle));
        return center + offset;
    }

    public void DestroyAgent()
    {
        _agent.enabled = false;
        _capsuleCollider.isTrigger = true;
    }

    private void TrySetDefaultPath()
    {
        if (_pathTargetList == null || _pathTargetList.Length == 0)
        {
            _pathTargetList = new Transform[1];
            _pathTargetList[0] = transform;
        }
    }
}


