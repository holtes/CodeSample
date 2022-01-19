using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HC_Structure
{
    public Centroid[] centroids;
}

[Serializable]
public class Centroid
{
    public string id;
    public MCD_Coordinate coordinates;
}

[Serializable]
public class HCL_Structure
{
    public HCL_Centroid[] centroids;
}

[Serializable]
public class HCL_Centroid
{
    public int id;
    public MCD_Coordinate coordinates;
}

