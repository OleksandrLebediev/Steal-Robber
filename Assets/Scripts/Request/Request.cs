using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Request
{
    [SerializeField] private int _numberOfTargets;
    [SerializeField] private ObjectForCollectType _objectForCollectType;
    [SerializeField] private Sprite _spriteOfTarget;

    public int NumberOfTargets => _numberOfTargets;
    public ObjectForCollectType ObjectForCollectType => _objectForCollectType;
    public Sprite SpriteOfTarget => _spriteOfTarget;
}
