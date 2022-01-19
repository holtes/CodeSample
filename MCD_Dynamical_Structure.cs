using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MCD_Dynamical_Structure
{
    public string header;
    public MCD_D_Station[] Map;
}

[Serializable]
public class MCD_D_Station
{
    public int id;
    public string color;
    public int value;
}