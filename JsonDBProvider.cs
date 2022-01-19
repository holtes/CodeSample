using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VH.jsonDB;
using Vizart;
// Автор: Mikhail Frenkel
// класс провайдера источника данных для базы данных JsonDB для работы в отдельном потоке
public class JsonDBProvider 
{

    // получить json строку 
    // FileName - имя файла json 
    public static string getJsonString(string FileName,int serverNum=2)
    {
        if (FileName == "config" ||
            FileName == "MetroDynamical" ||
            FileName == "monuments_memorials" ||
            FileName == "CPPK_static" ||
            FileName == "bkl_static" ||
            FileName == "centroids" ||         
            FileName == "centroids_late" ||
            FileName == "parking_zoom_2" ||         
            FileName == "parking_zoom_3" ||
            FileName == "hex_places" ||
            FileName == "Metro_static" ||
            FileName.Contains("local_dict"))         
            return LoadFile(FileName + ".json");
        else{ 
           if (JsonDB.generalConfig.isDebugData)  
               return LoadFile(VLocale.getLangJsonKey(FileName) + ".json");
           else  
               return JsonDB.jsonBD_data.getVal(FileName,serverNum);
        }
    }
    /// Loads the text file. (UTF-8)
    /// </summary>
    /// <returns>The file.</returns>
    /// <param name="path">Path.</param>
    private static string LoadFile(string path)
    {
            string dataPath = Application.streamingAssetsPath;
            dataPath = System.IO.Path.Combine(dataPath, "JsonDB/");
            string fileName = System.IO.Path.Combine(dataPath, path);
            Debug.Log($"Load file '{fileName}'");
            string result = "";
            if (fileName.Contains("://"))
            {
                WWW www = new WWW(fileName);
                while (!www.isDone) { }
                // if BOM in json UTF-8, nead skip BOM 
                if (www.bytes[0] == '\xEF' && www.bytes[1] == '\xBB' && www.bytes[2] == '\xBF')
                {
                    result = System.Text.Encoding.UTF8.GetString(www.bytes, 3, www.bytes.Length - 3);
                }
                else
                {
                    result = www.text;
                }
            }
            else
            {
                result = System.IO.File.ReadAllText(fileName,System.Text.Encoding.UTF8);
            }
             Debug.Log($"Read:'{fileName}'\n"+result);
            return result;               
    }
}
