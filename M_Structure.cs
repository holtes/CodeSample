using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class M_Structure
{
    public Metro[] Metro;
}

[Serializable]
public class Metro
{
    public MCD_Coordinate coordinates;
    public string id;
}