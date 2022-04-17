using System;
using UnityEngine;

[Serializable]
public class SaveData
{
    public SaveData(int level, int money)
    {
        Level = level;
        Money = money;
    }

    public int Level { get; }
    public int Money { get; }
}

