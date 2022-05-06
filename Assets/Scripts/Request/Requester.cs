using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioClip))]
public abstract class Requester : MonoBehaviour
{
    [SerializeField] private Request _request;
    [SerializeField] private RequestDisplay _requestDisplay;
    [SerializeField] private Receiver _receiver;

    private AudioSource _audioSource;
    private MonetaryRewardDispenser _rewardDispenser;
    private ISender _sender;
    private int _numberOfRemainingTargets;
    private int _amountMoneyHitTarget;
    

    public bool IsCompleted { get; private set; }
    public event UnityAction RequestCompleted;
    public event UnityAction AllMoneyPaid;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _rewardDispenser = GetComponentInChildren<MonetaryRewardDispenser>();
    }

    public void Initialize()
    {
        _receiver.Initialize(_request.ObjectForCollectType, _audioSource);
        _requestDisplay.Initialize(_request.NumberOfTargets, _request.SpriteOfTarget);
        _rewardDispenser.Initialize(_audioSource);
        _numberOfRemainingTargets = _request.NumberOfTargets;
    }

    private void OnEnable()
    {
        _receiver.ObjectAccepted += OnObjectAccepted;
        _rewardDispenser.MoneyMovedToTarget += OnMoneyMovedToTarget;
        _rewardDispenser.AllMoneyHitTarget += OnAllMoneyHitTarget; 
    }

    private void OnDestroy()
    {
        _receiver.ObjectAccepted -= OnObjectAccepted;
        _rewardDispenser.MoneyMovedToTarget -= OnMoneyMovedToTarget;
        _rewardDispenser.AllMoneyHitTarget -= OnAllMoneyHitTarget;
    }

    private void OnObjectAccepted(ISender sender)
    {
        if (_sender == null) _sender = sender;
        _numberOfRemainingTargets--;
        _rewardDispenser.DispenseMonetaryRewardToTarget(_request.Reward, transform.position, sender.CurrentTransform);
        _requestDisplay.UpdateAmountCollectObject(_numberOfRemainingTargets);
        CheckCompletionOfRequest();
    }

    private void CheckCompletionOfRequest()
    {
        if (_numberOfRemainingTargets == 0)
        {
            IsCompleted = true;
            _requestDisplay.SetCompletedDisplay();
            RequestCompleted?.Invoke();
        }
    }

    private void OnMoneyMovedToTarget()
    {
        _sender.Accepting.AddMoney(1);
    }

    private void OnAllMoneyHitTarget()
    {
        _amountMoneyHitTarget++;
        if (_amountMoneyHitTarget == _request.NumberOfTargets)
            AllMoneyPaid?.Invoke();
    }
}
