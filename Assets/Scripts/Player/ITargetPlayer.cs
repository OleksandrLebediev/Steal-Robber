using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetPlayer
{
    public bool IsDead { get; }
    public Transform Position { get; }
    public void TakeDamage(int damage);
}
