using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vizart;
namespace VH.jsonDB
{
    public class JsonDBKeys
    {
        public SortedDictionary<string, string> keysList = new SortedDictionary<string, string>();
        public JsonDBKeys()
        {
            //keysList["Metro"] = "metro_info";
            //keysList["EcoUDS"] = "eco/monitoring";
            //keysList["CPPK"] = "cppk_info";
            //keysList["Taxi"] = "taxi_info";
            //keysList["TrafficFlow"] = "traffic_flow";
            //keysList["Carsharing"] = "carsharing_info";
            //keysList["NGPT"] = "ngpt_info";
            //keysList["CityMobilityIndex"] = "city_mobility_index";
            //keysList["MCD"] = "mcd_info";
            keysList["jams highways"] = "traffic01";
            keysList["jams"] = "traffic23";
        }
        public string getKeyVal(string key)
        {
            //if(keysList.ContainsKey(key)) return keysList[key];
            return VLocale.getLangJsonKey(key.ToLower());
        }
    }
}
