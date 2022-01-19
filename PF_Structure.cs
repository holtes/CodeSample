using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PF_Structure
{
    public PF_District[] MatrixData;
}



[Serializable]
public class PF_District
{
    public PF_DistrictData[] from;
    public PF_DistrictData[] to;
    public string name;
    public string totalfrom;
    public string totalto;
}

[Serializable]
public class PF_DistrictData
{
    public string name;
    public string value;
}

