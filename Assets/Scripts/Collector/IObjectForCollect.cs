using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjectForCollect 
{
    public Transform CurrentTransform { get; }
    public ObjectForCollectType Type { get; }
    public bool IsCollected { get;}
    public void SetStateCollected();
    public void Remove();
}

public enum ObjectForCollectType
{
    Enemy,
    Animal,
    Hostage,
    Subject
}
