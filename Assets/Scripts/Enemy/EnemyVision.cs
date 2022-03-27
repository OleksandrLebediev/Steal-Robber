﻿using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{

    [SerializeField] private float _angle;
    [SerializeField] private float _radius;

    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private LayerMask _obstacleMask;

    private List<Transform> _visibleTargets = new List<Transform>();
    private int _delayUpdateFindTargets = 200;

    [SerializeField] private float _meshRay;
    [SerializeField] private int edgeResolveIterations;
    [SerializeField] private float edgeDstThreshold;
    [SerializeField] private float maskCutawayDst = .1f;


    [SerializeField] private MeshFilter _visionMeshFilter;
    private Mesh _visionMesh;

    private void Start()
    {
        _visionMesh = new Mesh();
        _visionMesh.name = "Vision Mesh";
        _visionMeshFilter.mesh = _visionMesh;

        FindVisibleTargets();
    }


    private void LateUpdate()
    {
        DrawVision();
    }

    private async void FindVisibleTargets()
    {
        while (true)
        {
            Collider[] currentVisibleTargets = Physics.OverlapSphere(transform.position, _radius, _targetMask);
            _visibleTargets.Clear();

            for (int i = 0; i < currentVisibleTargets.Length; i++)
            {
                Transform target = currentVisibleTargets[i].transform;
                Vector3 direction = (target.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, direction) < _angle / 2)
                {
                    float distance = Vector3.Distance(transform.position, target.position);
                    if (!Physics.Raycast(transform.position, direction, distance, _obstacleMask))
                    {
                        _visibleTargets.Add(target);
                    }
                }
            }
            await Task.Delay(_delayUpdateFindTargets);
        }
    }

    private void DrawVision()
    {
        int stepCount = Mathf.RoundToInt(_angle * _meshRay);
        float stepAngleSize = _angle / stepCount;
        List<Vector3> visionPoints = new List<Vector3>();
        VisionCastInfo oldVisionCast = new VisionCastInfo();

        for (int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.y - _angle / 2 + stepAngleSize * i;
            Debug.DrawLine(transform.position, transform.position + DirectionFromAngle(angle, true) * _radius, Color.red);
            VisionCastInfo newVisionCast = VisionCast(angle);
            visionPoints.Add(newVisionCast._point);

            if (i > 0)
            {
                bool edgeThresholdExceeded = Mathf.Abs(oldVisionCast._distance - newVisionCast._distance) > edgeDstThreshold;
                if (oldVisionCast._hit != newVisionCast._hit || (oldVisionCast._hit && newVisionCast._hit && edgeThresholdExceeded))
                {
                    EdgeInfo edge = FindEdge(oldVisionCast, newVisionCast);
                    if (edge._pointA != Vector3.zero)
                    {
                        visionPoints.Add(edge._pointA);
                    }

                    if (edge._pointB != Vector3.zero)
                    {
                        visionPoints.Add(edge._pointB);
                    }
                }

            }
            
            visionPoints.Add(newVisionCast._point);
            oldVisionCast = newVisionCast;
        }

        int vertexCount = visionPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(visionPoints[i]) + Vector3.forward * maskCutawayDst;

            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        _visionMesh.Clear();

        _visionMesh.vertices = vertices;
        _visionMesh.triangles = triangles;
        _visionMesh.RecalculateNormals();

    }


    EdgeInfo FindEdge(VisionCastInfo minViewCast, VisionCastInfo maxViewCast)
    {
        float minAngle = minViewCast._angle;
        float maxAngle = maxViewCast._angle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for (int i = 0; i < edgeResolveIterations; i++)
        {
            float angle = (minAngle + maxAngle) / 2;
            VisionCastInfo newViewCast = VisionCast(angle);

            bool edgeDstThresholdExceeded = Mathf.Abs(minViewCast._distance - newViewCast._distance) > edgeDstThreshold;
            if (newViewCast._hit == minViewCast._hit && !edgeDstThresholdExceeded)
            {
                minAngle = angle;
                minPoint = newViewCast._point;
            }
            else
            {
                maxAngle = angle;
                maxPoint = newViewCast._point;
            }
        }

        return new EdgeInfo(minPoint, maxPoint);
    }


    private VisionCastInfo VisionCast(float globalAngle)
    {
        Vector3 direction = DirectionFromAngle(globalAngle, true);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit, _radius, _obstacleMask))
        {
            return new VisionCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new VisionCastInfo(false, transform.position + direction * _radius, _radius, globalAngle);
        }
    }

    private Vector3 DirectionFromAngle(float angle, bool angleIsGlobal)
    {
        if (angleIsGlobal == false)
        {
            angle += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }

    public struct VisionCastInfo
    {
        public readonly bool _hit;
        public readonly Vector3 _point;
        public readonly float _distance;
        public readonly float _angle;

        public VisionCastInfo(bool hit, Vector3 point, float distance, float angle)
        {
            _hit = hit;
            _point = point;
            _distance = distance;
            _angle = angle;
        }
    }

    public struct EdgeInfo
    {
        public readonly Vector3 _pointA;
        public readonly Vector3 _pointB;

        public EdgeInfo(Vector3 pointA, Vector3 pointB)
        {
            _pointA = pointA;
            _pointB = pointB;
        }
    }

}