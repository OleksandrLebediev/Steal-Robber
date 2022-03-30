using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private Bag _bag;

    private readonly float _timeCollection = 0.7f;

    private float AnimationOfssetXPosition = -0.05f;
    private float AnimationOfssetYPosition;
    private float AnimationOfssetYRotation = 90;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<IObjectForCollect>(out IObjectForCollect itemForCollect) == true)
        {
            TryCollect(itemForCollect);
        }
    }

    private void TryCollect(IObjectForCollect itemForCollect)
    {
        if (itemForCollect.isCollected == true) return;

        CollectItemAnimation(itemForCollect);

    }

    private async void CollectItemAnimation(IObjectForCollect itemForCollect)
    {
        float currentTime = 0;
        Vector4 target = _bag.GetPositionFreeCall();

        itemForCollect.CurrentTransform.SetParent(transform);
        itemForCollect.SetStateCollected(); 
        _bag.AddObject(itemForCollect);

        AnimationCurve roadX = AnimationCurve.EaseInOut(0, itemForCollect.CurrentTransform.localPosition.x, _timeCollection, target.x + AnimationOfssetXPosition); 
        AnimationCurve roadY = AnimationCurve.EaseInOut(0, itemForCollect.CurrentTransform.localPosition.y, _timeCollection, target.y); 
        //AnimationCurve roadXRotation = AnimationCurve.EaseInOut(0, itemForCollect.CurrentTransform.localPosition.x, _timeCollection, -90f); 
        AnimationCurve roadYRotation = AnimationCurve.EaseInOut(0, itemForCollect.CurrentTransform.localPosition.y, _timeCollection, AnimationOfssetYRotation); 

        roadY.AddKey(_timeCollection / 4, 1.5f);
        roadY.AddKey((_timeCollection / 4) * 2, 1.5f);

        //roadXRotation.AddKey(_timeCollection / 4, 1.5f);
        //roadYRotation.AddKey((_timeCollection / 4) , 1.5f);

        AnimationCurve roadZ = AnimationCurve.EaseInOut(0, itemForCollect.CurrentTransform.localPosition.z, _timeCollection, target.z);
        //AnimationCurve scaleDelta = AnimationCurve.EaseInOut(0, 1, _timeCollection, .5f); 



        for (currentTime = 0; currentTime <= _timeCollection; currentTime += Time.deltaTime)
        {
            itemForCollect.CurrentTransform.localPosition = new Vector3(roadX.Evaluate(currentTime), roadY.Evaluate(currentTime), roadZ.Evaluate(currentTime));
            itemForCollect.CurrentTransform.localRotation = Quaternion.Euler(0, roadYRotation.Evaluate(currentTime), 0);
            //itemForCollect.CurrentTransform.localRotation = Quaternion.AngleAxis(ancle.Evaluate(currentTime), Vector3.up);
           // itemForCollect.CurrentTransform.localRotation = Quaternion.AngleAxis(ancle.Evaluate(currentTime), Vector3.up);
           // itemForCollect.CurrentTransform.localScale = new Vector3(scaleDelta.Evaluate(currentTime),  scaleDelta.Evaluate(currentTime), scaleDelta.Evaluate(currentTime)); // Временная схема
            await Task.Yield();
        }

        itemForCollect.CurrentTransform.localPosition = new Vector3(target.x + AnimationOfssetXPosition, target.y, target.z); // Установка кончных реультатов для кординат
       // itemForCollect.CurrentTransform.localRotation = Quaternion.AngleAxis(30, Vector3.up); // Установка кончных реультатов для поворота
        itemForCollect.CurrentTransform.localScale = new Vector3(1, 1, 1); // Установка клнечного размера     
    }


}
