using System;

[Serializable]
public class SI_Structure
{
    public SI_MapData[] Map;
    public DecreeStructure DecreeStructure;
    public DecreeCount DecreeCount;
}


[Serializable]
public class SI_MapData
{
    public string name;
    public string value;
    public string color;

}

[Serializable]
public class DecreeStructure
{
    public string header;
    public string subheader;
    public DS_Data[] data;
}

[Serializable]
public class DS_Data
{
    public string label;
    public string value;
    public string color;
}

[Serializable]
public class DecreeCount
{
    public string header;
    public DC_Data[] data;
}

[Serializable]
public class DC_Data
{
    public string id;
    public string label;
    public string value;
    public string color;
}