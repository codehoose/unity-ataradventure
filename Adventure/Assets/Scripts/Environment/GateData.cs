using System;
using UnityEngine;

[Serializable]
public class GateData
{
    public Vector2 position;
    public string key;

    [NonSerialized]
    public bool opened;
}