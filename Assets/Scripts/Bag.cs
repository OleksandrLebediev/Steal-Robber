using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bag : MonoBehaviour
{
    [SerializeField] private int _capacity;
    
    private List<IObjectForCollect> _objects = new List<IObjectForCollect>();
    private Vector3 _firstÑell = new Vector3(0, -0.37f, 0.15f);
    private float _stepBlockY = 0.16f;

    public int AmountObjects => _objects.Count;
    public int Capacity => _capacity;
    public bool isEmpty => _objects.Count == 0;
    public bool isFull => AmountObjects >= Capacity;
    public event UnityAction<int> ObjectsAmountChanged;
    public event UnityAction Desolated;
    public event UnityAction Filled;
    public event UnityAction<int> Changed; 

    public void AddObject(IObjectForCollect part)
    {
        if(isEmpty == true) Filled?.Invoke();
        _objects.Add(part);
        Changed?.Invoke(AmountObjects);
    }

    public IObjectForCollect TryGetCollectObject()
    {
        if (isEmpty == true) return null;
        IObjectForCollect item = _objects[AmountObjects - 1];
        return item;
    }

    public void RemoveCollectObject(IObjectForCollect objectForCollect)
    {
        _objects.Remove(objectForCollect);
        ObjectsAmountChanged?.Invoke(AmountObjects);
        if(isEmpty == true) Desolated?.Invoke();
    }

    public Vector3 GetPositionFreeCall()
    {
        Vector3 _currentÑell = _firstÑell;
        _currentÑell.y = AmountObjects * _stepBlockY + _firstÑell.y;
        return _currentÑell; 
    }
}
