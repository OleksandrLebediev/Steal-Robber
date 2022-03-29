using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bag : MonoBehaviour
{
    private List<IItemForCollect> _items = new List<IItemForCollect>();
    private Vector3 _firstÑell = new Vector3(0, -0.37f, 0.15f);
    private float _stepBlockY = 0.16f;

    public int AmountItems => _items.Count;
    public bool isEmpty => _items.Count == 0;

    public event UnityAction<int> PartAmountChanged;

    public void AddItem(IItemForCollect part)
    {
        _items.Add(part);
    }

    public void WithdrawnBlock()
    {
        IItemForCollect item = _items[AmountItems - 1];
        _items.Remove(item);
        //item.TryDestroy();
        PartAmountChanged?.Invoke(AmountItems);
    }

    public Vector3 GetPositionFreeCall()
    {
        Vector3 _currentÑell = _firstÑell;
        _currentÑell.y = AmountItems * _stepBlockY + _firstÑell.y;

        return _currentÑell; 
    }
}
