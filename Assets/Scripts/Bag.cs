using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bag : MonoBehaviour
{
    private List<IObjectForCollect> _objects = new List<IObjectForCollect>();
    private Vector3 _firstÑell = new Vector3(0, -0.37f, 0.15f);
    private float _stepBlockY = 0.16f;

    public int AmountObjects => _objects.Count;
    public bool isEmpty => _objects.Count == 0;

    public event UnityAction<int> ObjectsAmountChanged;
    public event UnityAction Desolated;
    public event UnityAction Filled;

    public void AddObject(IObjectForCollect part)
    {
        if(isEmpty == true) Filled?.Invoke();
        _objects.Add(part);
    }

    public IObjectForCollect GetCollectObject()
    {
        if (isEmpty == true) return null;

        IObjectForCollect item = _objects[AmountObjects - 1];
        _objects.Remove(item);
        ObjectsAmountChanged?.Invoke(AmountObjects);
        if(isEmpty == true) Desolated?.Invoke();
        return item;
    }

    public Vector3 GetPositionFreeCall()
    {
        Vector3 _currentÑell = _firstÑell;
        _currentÑell.y = AmountObjects * _stepBlockY + _firstÑell.y;

        return _currentÑell; 
    }
}
