using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform[] _pathTargetList;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;
    public Transform[] PathTargetList => _pathTargetList;

    private NavMeshAgent _agent;
    private int _currentPath;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void Initialize()
    {
        _agent.speed = _moveSpeed;
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

    public IEnumerator RotationCoroutine(Vector3 target, bool follow = false)
    {
        while (true)
        {
            Vector3 lookPos = target - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _rotateSpeed * Time.deltaTime);
            if(follow == true) yield return null;

            if (Quaternion.Angle(transform.rotation, rotation) <= 0.001f)
            {
                break;
            }
            yield return null;
        }
    }

    public IEnumerator RotationCoroutine(Transform target, bool follow = false)
    {
        while (true)
        {
            Vector3 lookPos = target.position - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _rotateSpeed * Time.deltaTime);
            if (follow == true) yield return null;

            if (Quaternion.Angle(transform.rotation, rotation) <= 0.001f)
            {
                break;
            }
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
}

