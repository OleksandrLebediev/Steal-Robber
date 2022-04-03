using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceptionZone : MonoBehaviour
{
    private ObjectForCollectType _objectForCollectType;
    private Transform _recipient;

    private readonly float _timeCollection = 0.7f;
    private readonly float _delayReceipt = 0.2f;


    [SerializeField] private AudioSource _audioSource;

    public virtual void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }


    public void Initialize(ObjectForCollectType objectForCollectType,  Transform recipient)
    {
        _objectForCollectType = objectForCollectType;
        _recipient = recipient;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            if (player.Bag.isEmpty == true) return;

            StartCoroutine(ReceptionCoroutine(player));
        }
    }

    private IEnumerator ReceptionCoroutine(Player player)
    {
        int amountObjects = player.Bag.AmountObjects;

        for (int i = 0; i < amountObjects; i++)
        {
            IObjectForCollect objectForCollect = player.Bag.GetCollectObject();
            if (objectForCollect.Type != _objectForCollectType) yield break;
            StartCoroutine(CollectObjectAnimation(objectForCollect));
            yield return new WaitForSeconds(_delayReceipt);
        }
    }

    private IEnumerator CollectObjectAnimation(IObjectForCollect objectForCollect)
    {
        float currentTime = 0;
        Vector3 target = _recipient.position;
        objectForCollect.CurrentTransform.SetParent(null);
        _audioSource.Play();

        AnimationCurve roadX = AnimationCurve.EaseInOut(0, objectForCollect.CurrentTransform.localPosition.x, _timeCollection, target.x);
        AnimationCurve roadY = AnimationCurve.EaseInOut(0, objectForCollect.CurrentTransform.localPosition.y, _timeCollection, target.y);

        roadY.AddKey(_timeCollection / 4, 1.5f);
        roadY.AddKey((_timeCollection / 4) * 2, 1.5f);



        AnimationCurve roadZ = AnimationCurve.EaseInOut(0, objectForCollect.CurrentTransform.localPosition.z, _timeCollection, target.z);

        for (currentTime = 0; currentTime <= _timeCollection; currentTime += Time.deltaTime)
        {
            objectForCollect.CurrentTransform.localPosition = new Vector3(roadX.Evaluate(currentTime), roadY.Evaluate(currentTime), roadZ.Evaluate(currentTime));
            yield return null;
        }

        objectForCollect.CurrentTransform.localPosition = new Vector3(target.x, target.y, target.z);                                                                                                                       // itemForCollect.CurrentTransform.localRotation = Quaternion.AngleAxis(30, Vector3.up); // Установка кончных реультатов для поворота
        objectForCollect.CurrentTransform.localScale = new Vector3(1, 1, 1);
    }
}
