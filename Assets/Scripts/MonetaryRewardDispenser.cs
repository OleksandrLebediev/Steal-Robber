using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonetaryRewardDispenser : MonoBehaviour
{
    [SerializeField] private Money _moneyTemplate;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private float _speed = 4;

    private List<Money> _moneyList = new List<Money>();
    private AudioSource _audioSource;
    private float _radius = 0.5f;
    private float _offsetY = 1;
    private float _delayMove = 1f;
    private Vector3 _offsetMoveTarget = Vector3.up;
    private float _soundDelay = 0.02f;
    private float _timeLastSound;


    public event UnityAction MoneyMovedToTarget;
    public event UnityAction AllMoneyHitTarget;

    public void Initialize(AudioSource audioSource)
    {
        _audioSource = audioSource;
    }

    public void DispenseMonetaryRewardToTarget(int amountOfMoney, Vector3 startPosition, Transform targetPosition)
    {
        for (int i = 0; i < amountOfMoney; i++)
        {
            Vector3 randomPosition = Random.insideUnitSphere * _radius + transform.position;
            randomPosition.y = _offsetY;
            Money money = Instantiate(_moneyTemplate, randomPosition, Quaternion.identity);
            _moneyList.Add(money);
        }

        StartCoroutine(MoveRewardToTargetCoroutine(targetPosition));
    }

    private IEnumerator MoveRewardToTargetCoroutine(Transform target)
    {
        yield return new WaitForSeconds(_delayMove);
         
        while (_moneyList.Count != 0)
        {
            for (int i = _moneyList.Count - 1; i >= 0; i--)
            {
                Money money = _moneyList[i];
                money.transform.position = Vector3.MoveTowards(money.transform.position, target.position + _offsetMoveTarget, _speed * Time.deltaTime);
                _speed += Time.deltaTime;
                money.DisablePhysics();

                if (Vector3.Distance(_moneyList[i].transform.position, target.transform.position + _offsetMoveTarget) <= 0.01)
                {
                    Destroy(money.gameObject);
                    MoneyMovedToTarget?.Invoke();
                    _moneyList.Remove(money);

                    if ((_timeLastSound + _soundDelay) <= Time.time)
                    {
                        _timeLastSound = Time.time;
                        _audioSource.PlayOneShot(_audioClip);
                    }
                }
            }
            yield return null;
        }

        AllMoneyHitTarget?.Invoke();
    }
}
