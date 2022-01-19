using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MM_Structure
{
    public Monument_Memorial[] museums;
    public Monument_Memorial[] monuments_memorials;
}

[Serializable]
public class Monument_Memorial
{
    public string name;
    public List<float> coordinates;
}