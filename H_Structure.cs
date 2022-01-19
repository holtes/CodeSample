using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class H_Structure
{
    public Hexagon[] hexagons;
}

[Serializable]
public class Hexagon
{
    public string id;
    public Coordinate[] coordinates;
}