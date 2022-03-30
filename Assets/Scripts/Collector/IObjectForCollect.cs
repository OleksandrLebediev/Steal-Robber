using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjectForCollect 
{
    public Transform CurrentTransform { get; }
    public bool isCollected { get;}
    public ObjectForCollect Type { get; }
    public void SetStateCollected();
}

public enum ObjectForCollect
{
    Enemy,
    Animal,
    Hostage,
    Subject
}
