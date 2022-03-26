using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemForCollect 
{
    public Transform CurrentTransform { get; }
    public bool isCollected { get;}
    public void SetStateCollected();
}
