using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class V_Structure
{
    public string header;
    public V_Data[] Map;
}

[Serializable]
public class V_Data
{
    public string color;
    public Coordinate coordinates;
}