﻿using System;

[Serializable]
public class MapRoomData
{
    public int colour;
    public int e;
    public int n;
    public int s;
    public int shape;
    public int w;
    public ItemData[] objects;
    public GateData gate;
    public DragonData[] dragons;
}
