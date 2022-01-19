using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vizart;

namespace VH.jsonDB
{
    public class JsonBD_data
    {
        public SortedDictionary<string, string> dataList = new SortedDictionary<string, string>();
        public SortedDictionary<string, int> serversList = new SortedDictionary<string, int>();
        public SortedDictionary<string, DateTime> LastIOList = new SortedDictionary<string, DateTime>();

        public void add(string key, string val,int serverNum)
        {
            if(string.IsNullOrEmpty(val) && dataList.ContainsKey(key)) return; // 
            dataList[key] = val;
            serversList[key] =serverNum;
        }
        public int getServerNum(string key){
            if(serversList.ContainsKey(key)) return serversList[key];
            return 2; 
        }
        public string getVal(string key,int serverNum=2)
        {
            regTime(key);
         //   string keyLang=VLocale.getLangJsonKey(key.ToLower());
            if (!dataList.ContainsKey(key)) 
                   add(key,JsonDB.GetJsonData(key,serverNum),serverNum); // регистрируем ключь и читаем первые данные
       //     Debug.Log("JSON: '"+key+"'\n"+ dataList[key]);
            return dataList[key];
        }
        private void regTime(string key){
           LastIOList[key]=DateTime.Now; 
        }
        // возвращает массив ключей сортированных по убываению времени
        public List<string> getTimeOrderKeys(){
            List<string> ret= new List<string>();
            string nKey=getMaxKey(ret);
            while(nKey!=""){
                ret.Add(nKey);
                nKey=getMaxKey(ret);
            }
            return ret;
        }
        private bool isKeyInList(string key,List<string> keys){
            for(int i=0;i<keys.Count;i++) if(keys[i]==key) return true;
            return false;
        }
        private string getMaxKey(List<string> keys){
            DateTime MaxDate= DateTime.MinValue;
            string retKey="";         
            foreach(KeyValuePair<string, DateTime> entry in LastIOList)
            {                
                if(isKeyInList(entry.Key,keys)) continue;
                if(MaxDate<entry.Value) {
                    MaxDate=entry.Value;
                    retKey=entry.Key;
                }
            }
            return retKey; 
        }
    }
}
