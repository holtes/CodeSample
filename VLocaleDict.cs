using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using  VH;

namespace Vizart{
public class VLocaleDict 
{   
    private  SortedDictionary<string, string> dataList = new SortedDictionary<string, string>();
    private string JsonFileName; // Имя файла для загрузки
    public  VLocale.langs lang= VLocale.langs.none;
    public string name ="###";
    public  VLocaleDict(VLocale.langs plang){
        lang=plang;
        JsonFileName="local_dict"+lang.ToString();
        if(!Load()) Debug.LogWarning($"Error load dict: {JsonFileName}");
    } 
    public void Clear(){
        dataList.Clear();
    }
    public void add(string key, string val)
    {
        if(string.IsNullOrEmpty(key)) return;
//        Debug.Log($"Dict add dataList[{key}] = {val}");
        dataList[key] = val;
    }

    public string getVal(string key)
    {
        string ret=key;
        if (dataList.ContainsKey(key)) ret=dataList[key];
        else
           Debug.LogWarning($"В словаре нет значения для ключа:'{key}'");        
//        Debug.Log($"getVal({key}) ==> '{ret}'");
        return ret;
    }

    public bool Load(){
            Clear();
            string jsonSrc = JsonDBProvider.getJsonString(JsonFileName);
            if (string.IsNullOrEmpty(jsonSrc)) return false;
            JSONNode N = JSON.Parse(jsonSrc);
            JSONNode N_Dict = N["dict"];
            if (N_Dict == null) return false;
            name=N["name"].Value;    
            int i=0;
            JSONNode N_Dicti=N_Dict[i];
            while(N_Dicti!=null){  
               add(N_Dicti["key"].Value,N_Dicti["value"].Value);
               i++;
               N_Dicti=N_Dict[i];
            }
            Debug.Log($"Load dict {JsonFileName} - {i} items");
            return true;            
    }
 
}
}