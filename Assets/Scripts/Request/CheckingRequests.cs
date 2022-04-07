using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckingRequests : MonoBehaviour
{
    [SerializeField] private Requester[] _requesters;

    private int _totalNumberOfRequests => _requesters.Length;
    private int _numberOfCompletedRequests;

    public event UnityAction AllRequestsCompleted;

    private void OnEnable()
    {
        foreach (var requester in _requesters)
        {
            requester.RequestCompleted += OnRequestCompleted;
        }
    }

    private void OnDisable()
    {
        foreach (var requester in _requesters)
        {
            requester.RequestCompleted -= OnRequestCompleted;
        }
    }

    private void OnRequestCompleted()
    {
        _numberOfCompletedRequests++;
        CheckCompletedOfAllRequests();
    }

    private void CheckCompletedOfAllRequests()
    {
        if (_numberOfCompletedRequests == _totalNumberOfRequests)
        {
            AllRequestsCompleted?.Invoke();
        }
    }
}
