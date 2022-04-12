using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MonetaryRewardDispenser))]
[RequireComponent(typeof(AudioClip))]
public abstract class Requester : MonoBehaviour
{
    [SerializeField] private Receiver _receiver;
    [SerializeField] private Request _request;
    [SerializeField] private RequestDisplay _requestDisplay;
    [SerializeField] private int _monetaryReward;

    private AudioSource _audioSource;
    private MonetaryRewardDispenser _rewardDispenser;
    private int _numberOfRemainingTargets;

    public bool IsCompleted { get; private set; }

    public event UnityAction RequestCompleted;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _rewardDispenser = GetComponent<MonetaryRewardDispenser>();
    }

    private void Start()
    {
        _receiver.Initialize(_request.ObjectForCollectType);
        _requestDisplay.Initialize(_request.NumberOfTargets, _request.SpriteOfTarget);
        _rewardDispenser.Initialize(_audioSource);
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

    private void OnObjectAccepted(ISender sender)
    {
        _numberOfRemainingTargets--;
        _rewardDispenser.DispenseMonetaryRewardToTarget(_monetaryReward, transform.position, sender.CurrentTransform);
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
