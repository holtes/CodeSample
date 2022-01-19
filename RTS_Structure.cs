using System;
using System.Collections.Generic;


[Serializable]
public class RTS_Structure
{
    public MainIndex MainIndex;
    public ForecastIndex ForecastIndex;
    public Speed Speed;
    public Accidents Accidents;
    public RoadWorks RoadWorks;
    public RTS_Info Info;
}

[Serializable]
public class MainIndex
{
    public Current current;
    public RTS_Forecast forecast;
    public MI_Info info;
}

[Serializable]
public class Current
{
    public string value;
    public string color;
    public string positivetrend;
}

[Serializable]
public class RTS_Forecast
{
    public string label;
    public string value;
    public string color;
}

[Serializable]
public class MI_Info
{
    public string color;
    public string value;
    public string arrow;
}

[Serializable]
public class ForecastIndex
{
    public string header;
    public DataForecast[] data;
}

[Serializable]
public class DataForecast
{
    public string id;
    public string label;
    public string value;
    public string color;
}

[Serializable]
public class Speed
{
    public string header;
    public DataSpeed[] data;
}

[Serializable]
public class DataSpeed
{
    public string id;
    public string label;
    public string value;
    public string color;
}

[Serializable]
public class BaseIcon
{
    public string header;
    public string value;
    public Deviation[] deviation;
    public Coordinates[] coordinates;

}

[Serializable]
public class Deviation
{
    public int id;
    public string label;
    public string value;
    public string color;
}

[Serializable]
public class Coordinates
{
    public string name;
    public Coordinate[] values;
}

[Serializable]
public class Coordinate
{
    public string lat;
    public string lon;
}

[Serializable]
public class Accidents : BaseIcon
{
}

[Serializable]
public class RoadWorks : BaseIcon
{
}

[Serializable]
public class RTS_Info
{
    public Weather Weather;
    public TrafficLegend[] TrafficLegend;
}

[Serializable]
public class Weather
{
    public string Temperature;
    public string icon;
}

[Serializable]
public class TrafficLegend
{
    public string id;
    public string label;
    public string color;
}
