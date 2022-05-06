using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReceptionZone : MonoBehaviour
{
    [SerializeField] private AudioClip _collectSound;

    private ObjectForCollectType _objectForCollectType;
    private AudioSource _audioSource;
    private Transform _recipient;

    private readonly float _timeCollection = 0.7f;
    private readonly float _delayReceipt = 0.2f;

    public event UnityAction<ISender> ObjectAccepted;

    public void Initialize(ObjectForCollectType objectForCollectType,  Transform recipient, AudioSource audioSource)
    {
        _objectForCollectType = objectForCollectType;
        _recipient = recipient;
        _audioSource = audioSource;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ISender>(out ISender sender))
        {
            if (sender.Bag.isEmpty == true) return;

            StartCoroutine(ReceptionCoroutine(sender));
        }
    }

    private IEnumerator ReceptionCoroutine(ISender sender)
    {
        int amountObjects = sender.Bag.AmountObjects;

        for (int i = 0; i < amountObjects; i++)
        {
            IObjectForCollect objectForCollect = sender.Bag.TryGetCollectObject();
            if (objectForCollect == null) yield break;
            if (objectForCollect.Type != _objectForCollectType) yield break;
            StartCoroutine(CollectObjectAnimation(objectForCollect, sender));
            yield return new WaitForSeconds(_delayReceipt);
        }
    }

    private IEnumerator CollectObjectAnimation(IObjectForCollect objectForCollect, ISender sender)
    {
        float currentTime = 0;
        Vector3 target = _recipient.position;
        objectForCollect.CurrentTransform.SetParent(null);
        _audioSource.PlayOneShot(_collectSound);
        sender.Bag.RemoveCollectObject(objectForCollect);
        
        var localPosition = objectForCollect.CurrentTransform.localPosition;
        AnimationCurve roadX = AnimationCurve.EaseInOut(0, localPosition.x, _timeCollection, target.x);
        AnimationCurve roadY = AnimationCurve.EaseInOut(0, localPosition.y, _timeCollection, target.y);

        roadY.AddKey(_timeCollection / 4, 1.5f);
        roadY.AddKey((_timeCollection / 4) * 2, 1.5f);

        AnimationCurve roadZ = AnimationCurve.EaseInOut(0, localPosition.z, _timeCollection, target.z);

        for (currentTime = 0; currentTime <= _timeCollection; currentTime += Time.deltaTime)
        {
            objectForCollect.CurrentTransform.localPosition = new Vector3(roadX.Evaluate(currentTime), roadY.Evaluate(currentTime), roadZ.Evaluate(currentTime));
            yield return null;
        }

        objectForCollect.CurrentTransform.localPosition = new Vector3(target.x, target.y, target.z);                                                                                                                       // itemForCollect.CurrentTransform.localRotation = Quaternion.AngleAxis(30, Vector3.up); // ��������� ������� ���������� ��� ��������
        objectForCollect.CurrentTransform.localScale = new Vector3(1, 1, 1);
        objectForCollect.Remove();
        ObjectAccepted?.Invoke(sender);
    }
}
