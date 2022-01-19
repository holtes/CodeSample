using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MD_Structure
{
    public string header;
    public MD_Map[] Map;
}

[Serializable]
public class MD_Map
{
    public string id;
    public string color;
    public int value;
}




