using System;

[Serializable]
public class P_Structure
{
    public P_Map[] Map;
}

[Serializable]
public class P_Map
{
	public P_Map_Data[] data;
	public string name;
}

[Serializable]
public class P_Map_Data
{
	public string color;
	public P_Coordinate coordinates;
}

[Serializable]
public class P_Coordinate
{
    public float lat;
    public float lon;
}

[Serializable]
public class PS_Structure
{
    public PS_Data[] ParkingZones;
}

[Serializable]
public class PS_Data
{
    public Coordinate coordinates;
    public string id;
}


[Serializable]
public class PS_R_Structure
{
    public PS_R_Rayoni Rayoni;
}

[Serializable]
public class PS_R_Rayoni
{
    public PS_R_Data[] data;
}


[Serializable]
public class PS_R_Data
{
    public MCD_Coordinate coordinates;
    public int id;
    public string name;
}



[Serializable]
public class PD_Structure
{
    public string header;
    public PD_MapCity[] MapCity;
    public PD_MapRayoni[] MapRayoni;
    public PD_MapZone[] MapZones;
}

[Serializable]
public class PD_MapCity
{
    public string name;
    public int id;
    public string value;
}


[Serializable]
public class PD_MapRayoni
{
    public string color;
    public int id;
}

[Serializable]
public class PD_MapZone
{
    public string color;
    public string id;
}


