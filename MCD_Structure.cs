using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MCD_Structure
{
    public MCD_Station[] MCD;
}

[Serializable]
public class MCD_Station
{
    public string id;
    public MCD_Coordinate coordinates;
}

[Serializable]
public class MCD_Coordinate
{
    public float lat;
    public float lon;
}

