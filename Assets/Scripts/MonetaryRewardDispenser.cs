using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonetaryRewardDispenser : MonoBehaviour
{
    [SerializeField] private Money _moneyTemplate;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private float _speed = 4;
    
    private AudioSource _audioSource;
    private float _timeLastSound;
    private readonly float _soundDelay = 0.02f;
    
    private readonly float _radius = 0.5f;
    private readonly float _offsetY = 1;
    private readonly float _delayMove = 1f;
    private readonly Vector3 _offsetMoveTarget = Vector3.up;

    public event UnityAction MoneyMovedToTarget;
    public event UnityAction AllMoneyHitTarget;

    public void Initialize(AudioSource audioSource)
    {
        _audioSource = audioSource;
    }

    public void DispenseMonetaryRewardToTarget(int amountOfMoney, Vector3 startPosition, Transform targetPosition)
    {
        List<Money> monies = new List<Money>();

        for (int i = 0; i < amountOfMoney; i++)
        {
            Vector3 randomPosition = Random.insideUnitSphere * _radius + transform.position;
            randomPosition.y = _offsetY;
            Money money = Instantiate(_moneyTemplate, randomPosition, Quaternion.identity);
            monies.Add(money);
        }

        StartCoroutine(MoveRewardToTargetCoroutine(targetPosition, monies, _speed));
    }

    private IEnumerator MoveRewardToTargetCoroutine(Transform target, List<Money> monies, float speed)
    {
        yield return new WaitForSeconds(_delayMove);
         
        while (monies.Count != 0)
        {
            for (int i = monies.Count - 1; i >= 0; i--)
            {
                Money money = monies[i];
                money.transform.position = Vector3.MoveTowards(money.transform.position, 
                    target.position + _offsetMoveTarget, speed * Time.deltaTime);
                speed += Time.deltaTime;
                money.DisablePhysics();

                if (Vector3.Distance(monies[i].transform.position, target.transform.position + _offsetMoveTarget) <= 0.01)
                {
                    Destroy(money.gameObject);
                    MoneyMovedToTarget?.Invoke();
                    monies.Remove(money);

                    if ((_timeLastSound + _soundDelay) <= Time.time)
                    {
                        _timeLastSound = Time.time;
                        _audioSource.PlayOneShot(_audioClip);
                    }
                }
            }
            yield return null;
        }
        monies.Clear(); 
        AllMoneyHitTarget?.Invoke();
    }
}
