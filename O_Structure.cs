using System;

[Serializable]
public class O_Structure
{
    public O_RoadTraffic RoadTraffic;
}

[Serializable] public class O_DefaultClass { public string header; }

[Serializable] public class O_DefaultClassWithLegend : O_DefaultClass { public string legend; }

[Serializable] public class O_DefaultDataWithSubheader : O_DefaultData { public string subheader; }

[Serializable]
public class O_DefaultClassWithValue : O_DefaultClass
{
    public string subheader;
    public string value;
}

[Serializable]
public class O_DefaultClassWithColor : O_DefaultClass
{
    public string subheader;
    public string value;
    public string color;
}

[Serializable]
public class O_DefaultData
{
    public int id;
    public string color;
    public string label;
    public string value;
}


[Serializable]
public class O_SmallData
{
    public int id;
    public int value;
}

[Serializable]
public class O_StringData
{
    public string label;
    public string value;
}

[Serializable] public class O_StringDataWithColor : O_StringData { public string color; }

[Serializable] public class O_SmallDataString : O_SmallData { new public string value; }
[Serializable] public class O_DefaultClassStringData : O_DefaultClass { public O_StringData[] data; }

[Serializable]
public class O_SmallDataColor
{
    public string color;
    public O_SmallData[] values;
}

[Serializable]
public class O_AxisData
{
    public O_SmallDataString[] x;
    public O_SmallDataString[] y;
}

[Serializable]
public class O_DefaultClassDiagramWithLegend : O_DefaultClassWithLegend
{
    public O_SmallDataColor data;
    public O_AxisData axisdata;
}

[Serializable] public class O_DefaultClassWithData : O_DefaultClass { public O_DefaultData[] data; }
[Serializable] public class O_DefaultClassWithDataSubheader : O_DefaultClass { public O_DefaultDataWithSubheader[] data; }
[Serializable] public class O_DefaultClassWithCircleDiagram : O_DefaultClass { public O_StringDataWithColor[] data; }


[Serializable] public class O_TrafficLoad : O_DefaultClassWithData { }
[Serializable] public class O_TrafficForecast : O_DefaultClassWithData { }
[Serializable] public class O_SpeedCity : O_DefaultClassWithDataSubheader { }
[Serializable] public class O_SpeedDynamics : O_DefaultClassDiagramWithLegend { }
[Serializable] public class O_ParkingLoad : O_DefaultClassWithColor { }
[Serializable] public class O_ParkingZones : O_DefaultClassStringData { }
[Serializable] public class O_CarsCount : O_DefaultClassWithValue { }
[Serializable] public class O_CarsStructure : O_DefaultClassWithCircleDiagram { }




[Serializable]
public class O_RoadTraffic
{
    public O_TrafficLoad TrafficLoad;
    public O_TrafficForecast TrafficForecast;
    public O_SpeedCity SpeedCity;
    public O_SpeedDynamics SpeedDynamics;
    public O_ParkingLoad ParkingLoad;
    public O_ParkingZones ParkingZones;
    public O_CarsCount CarsCount;
    public O_CarsStructure CarsStructure;
}

