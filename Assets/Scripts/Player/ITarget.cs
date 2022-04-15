using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITarget: ILocation, IPosition
{
    public bool IsDead { get; }
    public void TakeDamage(int damage);
}

public interface ILocation
{
    public Transform CurrentTransform { get; }
}

public interface ISender : ILocation
{
    public Bag Bag { get; }
    public IAcceptingMoney Accepting {get;}
}

