
using UnityEngine;

public interface IShooter : ITargeter
{
    public Weapon Weapon { get; }
    public ITarget Target { get; }
}

public interface ITargeter
{
    public IPosition TargetPosition { get; }
}

public interface IPosition
{
    public Vector3 VectorPosition { get; }
}