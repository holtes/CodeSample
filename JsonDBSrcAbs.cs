using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Автор: Mikhail Frenkel
// абстрактный класс источника данных для базы данных JsonDB
namespace VH.jsonDB
{

    public abstract class JsonDBSrcAbs
    {
        public string JsonFileName; // Имя файла для загрузки
        public string mainTitle; // Общий заголовок 
        public string getJsonTxt(int ServerNum=1){
            return JsonDBProvider.getJsonString(JsonFileName,ServerNum);
        }
        //====== Методы для реализации наследниками
        public abstract void Load();
        public abstract void Clear();
    }
}