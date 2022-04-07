using UnityEngine;
using UnityEngine.Events;

public abstract class Requester : MonoBehaviour
{
    [SerializeField] private Receiver _receiver;
    [SerializeField] private Request _request;
    [SerializeField] private RequestDisplay _requestDisplay;

    public bool IsCompleted { get; private set; }

    private int _numberOfRemainingTargets;
    public event UnityAction RequestCompleted;

    private void Start()
    {
        _receiver.Initialize(_request.ObjectForCollectType);
        _requestDisplay.Initialize(_request.NumberOfTargets, _request.SpriteOfTarget);
        _numberOfRemainingTargets = _request.NumberOfTargets;
    }

    private void OnEnable()
    {
        _receiver.ObjectAccepted += OnObjectAccepted;
    }

    private void OnDestroy()
    {
        _receiver.ObjectAccepted -= OnObjectAccepted;
    }

    private void OnObjectAccepted()
    {
        _numberOfRemainingTargets--;
        _requestDisplay.UpdateAmountCollectObject(_numberOfRemainingTargets);
        CheckCompletionOfRequest();
    }

    private void CheckCompletionOfRequest()
    {
        if (_numberOfRemainingTargets == 0)
        {
            IsCompleted = true;
            RequestCompleted?.Invoke();
        }
    }
}
