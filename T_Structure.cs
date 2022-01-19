using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class T_Structure
{
    public string header;
    public Hex_Data[] Map;
}

[Serializable]
public class Hex_Data
{
    public string id;
    public int value;
}

