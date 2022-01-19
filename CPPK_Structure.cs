using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CPPK_Structure
{
    public CPPK_Station[] CPPK;
}

[Serializable]
public class CPPK_Station
{
    public float id;
    public CPPK_Coordinate coordinates;
}

[Serializable]
public class CPPK_Coordinate
{
    public float lat;
    public float lon;
}

[Serializable]
public class CPPK_D_Structure
{
    public string header;
    public CPPK_D_Station[] Map;
}

[Serializable]
public class CPPK_D_Station
{
    public string color;
    public int id;
    public int value;
}


