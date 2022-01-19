using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VH.MultiDataView;
using VH.FileView;
using VH.jsonDB;
/* Автор: Mikhail Frenkel
 абстрактный класс  обработчика событий о изменении уровня положения камеры
Для каждой панели своя реализация. 
Реализация вешается как компонент на панель.
 */
public abstract class LevelChangeAbc : MonoBehaviour
{
    public MultiDataViewPanelUI multiDataViewPanelUI;
    public LeftBarPanelUI leftBarPanelUI;
    public FileViewPanelUI fileViewPanelUI;
    [HideInInspector]
    public int currentZoomLevel=0;
    [HideInInspector]
    public float currentZoomVal;
    [HideInInspector]
    public string currentSelectedOkrug="";

    [HideInInspector]
    public bool isLoadData = false; // требуеться перегрузить данные
    [HideInInspector]
    public bool isUseOkrugSelect = false; // требуеться перегрузить данные при каждом изменении округа

    [HideInInspector]
    public bool isStart = false;
    public void setZoom_base(int zoomLevel, float zoomVal)
    {
        currentZoomLevel = zoomLevel;
        currentZoomVal = zoomVal;
    }
    public void onDataChange()
    {
        isLoadData = false;
    }
    void Awake()
    {
        Init();   
    }
    protected void Init()
    {
    //    Debug.Log("LevelChangeAbc  Awake()");
        JsonDB.OnDataChanged += onDataChange;
    }
     
    //====== Методы для реализации наследниками

    //Метод  вызывается при активации панели и каждый раз при изменении уровня от камеры.
    // zoomLevel - Уровень зума камеры
    // zoomVal - значение z - кординаты камеры
    public abstract void setZoom(int zoomLevel, float zoomVal);
    //Метод  вызывается при активации панели и каждый раз при изменении от камеры - на какой округ она смотрит.
    // selectedOkrug - имя выбранного округа
    public abstract void setOkrug( string selectedOkrug);

}
