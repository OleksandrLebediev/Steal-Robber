using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITarget: ILocation
{
    public bool IsDead { get; }
    public void TakeDamage(int damage);
}

public interface ILocation
{
    public Transform Position { get; }
}

public interface ISender: ILocation
{
    public Bag Bag { get; }
}
