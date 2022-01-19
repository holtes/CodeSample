using System;
using System.IO;
using System.Collections;
using System.Net;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vizart;
using SimpleJSON;
// Автор: Mikhail Frenkel
// Класс базы данных основанной на чтение json файлов
// на сцене может быть только один экземпляр этого класса
// статическая переменная me1 его содержит

// Класс содержит список источников (наследников от JsonDBSrcAbs)
// регистрация источников делается на Awake() соответствующей панели вызовом метода add()
// на старте класс проводит общею загрузку.

namespace VH.jsonDB
{
    public enum jsonDB_NetResponseType
    {
        Login, jsonData
    }
    public enum jsonDB_NetError
    {
        Ok, inProgress, HttpSyntax, Accesss, Connect, unNoneError
    }

    public class JsonDB : MonoBehaviour
    {
        public static JsonDB me1;

        private static List<JsonDBSrcAbs> SrcList = new List<JsonDBSrcAbs>();

        public static JsonBD_data jsonBD_data= new JsonBD_data();
        public static jsonDB_NetError httpStatus = jsonDB_NetError.Ok;
        private static JsonDBKeys jsonDBKeys = new JsonDBKeys();
        public static GeneralConfig generalConfig = new GeneralConfig();
        private static float startTime = 0f;
        public delegate void dataChanged();
        public static event dataChanged OnDataChanged;
        public static int numLoadOk=0;
        public static int numLoadErr=0;
        private static bool isConfigLoad=false;
        private void Awake()
        {
            //  Debug.Log("JsonDB  Awake()");
            if (me1 == null)
            {
                me1 = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
           // Debug.Log("JsonDB  Start()");
            //LoadAll();
            startTime = Time.time;
        }

        // Update is called once per frame
        void Update()
        {
            // отслеживать тамер оновления базы
            if(generalConfig.RefreshTimeOut>0 && (Time.time - startTime) > (float)generalConfig.RefreshTimeOut)
            {
                RefreshAll();
            }
        }
        public void RefreshAll(){

                startTime = Time.time;
                // запуск в отдельном потоке
                System.Threading.Thread nThr = new System.Threading.Thread(RefreshInThread);
                nThr.Start(this);
        }
        private void RefreshInThread(object obj)
        {
            numLoadErr=0;
            numLoadOk=0;
            JsonDB me1i = obj as JsonDB;
            GetJsonData();
            me1i.LoadAll();
            OnDataChanged?.Invoke();
            if(numLoadOk==0) Indicator.setStatus(Indicator.statusList.noConnect);
            else{
              if(numLoadErr==0) Indicator.setStatus(Indicator.statusList.Connect);
              else Indicator.setStatus(Indicator.statusList.ConnectWithError);  
            }
        }

        public static GeneralConfig loadGeneralConfig()
        { 
            if(!isConfigLoad){ 
                generalConfig = JsonUtility.FromJson<GeneralConfig>(JsonDBProvider.getJsonString("config"));
                Debug.Log("Read GeneralConfig from config.json");
                Debug.Log(JsonUtility.ToJson(generalConfig));
            }
            isConfigLoad=true;
            return generalConfig;
        }
        public void add(JsonDBSrcAbs newSrc)
        {
            SrcList.Add(newSrc);
        }

        public T getFirstJsonSrc<T>() where T : class
        {
            for (int i = 0; i < SrcList.Count; i++) if (SrcList[i].GetType() == typeof(T))
                    return  (T) Convert.ChangeType(SrcList[i], typeof(T));
            return default(T);
        }
        public void LoadAll()
        {   
            
       //     Debug.Log("JsonDB LoadAll: "+ SrcList.Count);
            // @todo пуск в отдельном потоке
            for (int i=0;i<SrcList.Count;i++)
            { try
               {
                SrcList[i].Load();
               }catch(UnityException ue){
                   Debug.LogWarning("Exception on load:" +SrcList[i].ToString()+"\n"+ ue.Message);
               }
                catch(Exception ex){
                   Debug.LogWarning("Exception on load:" +SrcList[i].ToString()+"\n"+ ex.Message);
               }
            }
            
        }
        public void ClearAll()
        {
            foreach (JsonDBSrcAbs cSrc in SrcList)
            {
                cSrc.Clear();
            }
        }
        public void Clear()
        {
            SrcList.Clear();
        }
        public static void GetJsonData()
        {
            List<string> KeysCol = jsonBD_data.getTimeOrderKeys(); //new List<string>();
        //    foreach (KeyValuePair<string, string> iKey in jsonBD_data.dataList) KeysCol.Add(iKey.Key);
            Debug.Log("GetJsonData for: "+ KeysCol.Count);
            for (int i=0;i< KeysCol.Count;i++) jsonBD_data.add(KeysCol[i], GetJsonData(KeysCol[i]),jsonBD_data.getServerNum(KeysCol[i]));
        }
            public static string GetJsonData(string key,int serverNum=2)
        {
            string ret = "";
            JsonBD_NetResponse respons = getResponse(key,jsonDB_NetResponseType.jsonData,serverNum);
            if (respons.sReadData != "")
            {
                if(generalConfig.isSaveDBGJsonFile){
                    string fileName=generalConfig.SaveDBGJsonFilePath + VLocale.getLangJsonKey(key)+".json";
                    Debug.Log($"Сохранён '{fileName}'");                
                    File.WriteAllText(fileName, respons.sReadData);
                
                }
              //  Debug.Log("GetJsonData respons:\n" + respons.sReadData);

                ret=respons.sReadData;
                httpStatus = jsonDB_NetError.Ok;
            }
            else
                httpStatus = respons.NetStatus;
            // проверяем валидность json
            if(string.IsNullOrEmpty(ret))  return ret;
            try {
                JSONNode N = JSON.Parse(ret);
                if(N==null) ret="";
            } catch(Exception ee){
                ret="";
            }
            return ret;
        }
        public static JsonBD_NetResponse getResponse(string key, jsonDB_NetResponseType rType,int serverNum=2)
        {
            JsonBD_NetResponse ret = new JsonBD_NetResponse();
            WebClient wc = new WebClient();
            switch (rType)
            {
                case jsonDB_NetResponseType.Login:
                    wc.Headers["Content-Type"] = "application/json";
                    break;
                case jsonDB_NetResponseType.jsonData:
                    wc.Headers["Content-Type"] = "application/json";
                    wc.Headers["AUTHORIZATION"] = "Bearer " + generalConfig.BearerTokeh;
                    break;
            }

            try
            {
//                print(wc.DownloadString(getUrl(rType, key, serverNum)));
                ret.sReadData = wc.DownloadString(getUrl(rType,key,serverNum));
                numLoadOk+=1;
                Indicator.setStatus(Indicator.statusList.Connect);
            }
            catch (WebException e)
            {
                ret.errStatus = e.Status;
                ret.errMesage = e.Message;
                Debug.LogWarning("WebException WebClient.  key:'" + key + "', Status: " + ret.errStatus+ "\nMessage:'"+ret.errMesage+"'\n Time: " + System.DateTime.Now.ToString("hh:mm:ss") );
            }
            if (ret.errStatus != WebExceptionStatus.Success)
            {
                numLoadErr+=1;
                Indicator.setStatus(Indicator.statusList.noConnect);
                Debug.LogWarning("Error WebClient.  Key:'" + key + "', Status: " + ret.errStatus+ "\nMessage:'"+ret.errMesage+"'\n Time: " + System.DateTime.Now.ToString("hh:mm:ss") );
            }
            switch (ret.errStatus)
            {
                case WebExceptionStatus.Success:        // всё ОК
                    ret.NetStatus = jsonDB_NetError.Ok;
                    break;
                case WebExceptionStatus.ProtocolError:        // всё ОК                    
                    ret.NetStatus = jsonDB_NetError.Accesss;
                    break;

                default:                       //другие ошибки
                    ret.NetStatus = jsonDB_NetError.Connect;
                    
                    ret.sReadData = "";
                    break;
            }
            Debug.Log("Resive key:'" + key + "', Status: " + ret.errStatus+", Time: " + System.DateTime.Now.ToString("hh:mm:ss") );
            return ret;
        }

        public static string getUrl(jsonDB_NetResponseType rType,string key,int serverNum=2)
        {

            string ret = "";

            switch (rType)
            {
                case jsonDB_NetResponseType.Login:
                   // ret = serverUrl + "login?emailaddress=" + userNik + "&password=" + userPassword;
                    break;
                case jsonDB_NetResponseType.jsonData:
                    print(serverNum);
                    if (serverNum == 2)
                    {
                      //  print("TUT");
                        ret = generalConfig.server2Url + jsonDBKeys.getKeyVal(key);
                    }
                    else
                    {
                        ret = generalConfig.serverUrl + jsonDBKeys.getKeyVal(key);
                    }
                    break;
            }
            Debug.Log("Make URL:'" + ret + "'");
            return ret;
        }
      
    }
}