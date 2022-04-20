using System.Collections;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private Bag _bag;

    private readonly float _timeCollection = 0.7f;

    private float AnimationOfssetXPosition = -0.05f;
    private float AnimationOfssetYPosition;
    private float AnimationOfssetYRotation = 90;
    private bool _isDeactivated;

    private void OnTriggerEnter(Collider other)
    {
        if (_isDeactivated == true) return;

        if (other.TryGetComponent<IObjectForCollect>(out IObjectForCollect itemForCollect) == true)
        {
            TryCollect(itemForCollect);
        }
    }

    public void Deactivate()
    {
        _isDeactivated = true;
    }

    private void TryCollect(IObjectForCollect itemForCollect)
    {
        if (itemForCollect.IsCollected == true) return;
        StartCoroutine(CollectObjectAnimation(itemForCollect));
    }

    private IEnumerator CollectObjectAnimation(IObjectForCollect itemForCollect)
    {
        float currentTime = 0;
        Vector4 target = _bag.GetPositionFreeCall();

        itemForCollect.CurrentTransform.SetParent(transform);
        itemForCollect.SetStateCollected();
        _bag.AddObject(itemForCollect);

        AnimationCurve roadX = AnimationCurve.EaseInOut(0, itemForCollect.CurrentTransform.localPosition.x, _timeCollection, target.x + AnimationOfssetXPosition);
        AnimationCurve roadY = AnimationCurve.EaseInOut(0, itemForCollect.CurrentTransform.localPosition.y, _timeCollection, target.y);
        AnimationCurve roadYRotation = AnimationCurve.EaseInOut(0, itemForCollect.CurrentTransform.localPosition.y, _timeCollection, AnimationOfssetYRotation);

        roadY.AddKey(_timeCollection / 4, 1.5f);
        roadY.AddKey((_timeCollection / 4) * 2, 1.5f);

        AnimationCurve roadZ = AnimationCurve.EaseInOut(0, itemForCollect.CurrentTransform.localPosition.z, _timeCollection, target.z);

        for (currentTime = 0; currentTime <= _timeCollection; currentTime += Time.deltaTime)
        {
            itemForCollect.CurrentTransform.localPosition = new Vector3(roadX.Evaluate(currentTime), roadY.Evaluate(currentTime), roadZ.Evaluate(currentTime));
            itemForCollect.CurrentTransform.localRotation = Quaternion.Euler(0, roadYRotation.Evaluate(currentTime), 0);
            yield return null;
        }

        itemForCollect.CurrentTransform.localPosition = new Vector3(target.x + AnimationOfssetXPosition, target.y, target.z);
        itemForCollect.CurrentTransform.localScale = new Vector3(1, 1, 1);
        yield break;
    }


}
