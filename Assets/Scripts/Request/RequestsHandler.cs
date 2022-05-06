using UnityEngine;
using UnityEngine.Events;

public class RequestsHandler : MonoBehaviour, IRequestsReporter
{
    [SerializeField] private Requester[] _requesters;

    private int _totalNumberOfRequests => _requesters.Length;
    private int _numberOfCompletedRequests;
    private int _amountOfMoneyPaidRequests;

    public event UnityAction AllRequestsCompleted;

    private void Awake()
    {
        _requesters = GetComponentsInChildren<Requester>();
    }

    private void OnDisable()
    {
        foreach (var requester in _requesters)
        {
            requester.RequestCompleted -= OnRequestCompleted;
            requester.AllMoneyPaid -= OnAllMoneyPaid;
        }
    }

    public void Initialize()
    {
        foreach (var requester in _requesters)
        {
            requester.Initialize();
            requester.RequestCompleted += OnRequestCompleted;
            requester.AllMoneyPaid += OnAllMoneyPaid;
        }
    }

    private void OnRequestCompleted()
    {
        _numberOfCompletedRequests++;
        CheckCompletedOfAllRequests();
    }

    private void OnAllMoneyPaid()
    {
        _amountOfMoneyPaidRequests++;
        CheckCompletedOfAllRequests();
    }

    private void CheckCompletedOfAllRequests()
    {
        if (_numberOfCompletedRequests == _totalNumberOfRequests
            && _amountOfMoneyPaidRequests == _totalNumberOfRequests)
        {
            AllRequestsCompleted?.Invoke();
        }
    }
}
