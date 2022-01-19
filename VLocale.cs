using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using  VH.jsonDB;
using System.Globalization;
using UnityEngine.Video;
namespace Vizart{
public class VLocale : MonoBehaviour
{
    public enum langs{
       // 0 , 1 ,  2 , 3
        none,_ru,_en,_ar
    }
    [Header("SubMenu на контроле изменения языка")]
    [SerializeField]
    public SubMenu[] subMenuList;

    [Header("Текстовые объекты на контроле изменения языка")]
    [SerializeField]
    public VLocaleControlItem[] ControlTextList;
     [Header("объекты на сцене на контроле изменения языка")]
    [SerializeField]
    public VLocaleControlItem[] ControlObjList;
     [Header("Спрайты на сцене на контроле изменения языка")]
    [SerializeField]
    public VLocaleControlItem[] ControlSrpiteList;

     [Header("Видео плэйеры на сцене на контроле изменения языка")]
    public VLocaleControlItemVideoClip[] ControlVideoClipList;     
    public static VLocale me1;
    private static List<VLocaleDict> Dicts= new List<VLocaleDict>();
    private static langs curLang=langs._ru;
    private static VLocaleDict curDict;
    private static bool isInit=false;

    void Awake(){
       me1=this;
        if(!isInit)  Init();
    }
    public 
    // Start is called before the first frame update
    void Start(){    
        setLang(curLang);
    }

    // Update is called once per frame
   // void Update(){}
    void Init(){
        Dicts.Clear();
        Dicts.Add(new VLocaleDict(langs._ru));
        Dicts.Add(new VLocaleDict(langs._en));
        Dicts.Add(new VLocaleDict(langs._ar));
        isInit=true;
    }
    public static langs getLang(){
       return curLang;
    }

    public static bool setLang(langs nLang){
        if(me1==null) return false;
        curLang=nLang;
        curDict=me1.GetDict(curLang);
        if(curDict==null){
            Debug.LogWarning($"Error set lang '{nLang}'");
            return false;
        }
        Debug.Log($"Set lang '{curDict.name}'");
        me1.setCurLang();
        return true;
    }
    public void setCurLang(){
        // переписываем текст у всех subMenu
        for(int i=0;i<subMenuList.Length;i++) {
            SubMenu cItem=subMenuList[i];
            if(string.IsNullOrEmpty(cItem.langKeyheaderText)) cItem.langKeyheaderText=cItem.header;
            cItem.header=curDict.getVal(cItem.langKeyheaderText);
            // все подпункты меню
            for(int j=0;j<cItem.items.Length;j++){
                SubMenu.SubMenuItem sItem=cItem.items[j];
                if(string.IsNullOrEmpty(sItem.LangKey)) sItem.LangKey=sItem.subButtonT;
                sItem.subButtonT=curDict.getVal(sItem.LangKey);
            }
            cItem.reLoad(); 
        }

        // переписываем текст у всех текстовых объектов
        for(int i=0;i<ControlTextList.Length;i++) {
            VLocaleControlItem cItem=ControlTextList[i];
            Debug.Log(curDict.getVal(cItem.key));
            if(!cItem.isInit) 
            {
                cItem.key=cItem.CtlObj.GetComponent<Text>().text;
                
                cItem.isInit=true;
            }
            cItem.CtlObj.GetComponent<Text>().text=curDict.getVal(cItem.key); 
        }

        // заменяем спрайты всех объектов
        for(int i=0;i<ControlSrpiteList.Length;i++){
            VLocaleControlItem cItem=ControlSrpiteList[i];
            if(ControlSrpiteList[i].lang!=curLang) continue;
                try
                {
                    Sprite sp = Resources.Load<Sprite>(cItem.resourseFileName);
                    if (sp == null)
                    {
                        Debug.LogWarning("Не найден sprite в ресурсе: " + cItem.resourseFileName);
                        continue;
                    }
                    cItem.CtlObj.GetComponent<SpriteRenderer>().sprite = sp;
                }
                catch (MissingComponentException e)
                {
                    Sprite sp = Resources.Load<Sprite>(cItem.resourseFileName);
                    if (sp == null)
                    {
                        Debug.LogWarning("Не найден image в ресурсе: " + cItem.resourseFileName);
                        continue;
                    }
                    cItem.CtlObj.GetComponent<Image>().sprite = sp;

                }
        }
        // включение\выключение всех объектов
        for(int i=0;i<ControlObjList.Length;i++)  ControlObjList[i].CtlObj.SetActive(ControlObjList[i].lang==curLang);
          
        // переписываем виде клипы у всех  объектов
        for(int i=0;i<ControlVideoClipList.Length;i++) {
            VLocaleControlItemVideoClip cItem=ControlVideoClipList[i];
            if(!cItem.isInit) 
            {
                cItem.key=cItem.getKey();
                cItem.isInit=true;
            }
            cItem.loadClip(curDict.getVal(cItem.key)); 
        }

            // обновляем стартовый экран
        ZeroPointUI.me1.dashboardScreen.GetComponent<DashboardScreen>().UpdateScreenData();
 
    }
    
    public string Translate(string key)
    {
        return curDict.getVal(key);
    }

    private  VLocaleDict GetDict(langs lang){
        for(int i=0;i<Dicts.Count;i++) if(Dicts[i].lang==lang) return Dicts[i];
        return null;
    }
    public static string getLangJsonKey(string  jsonKey){
        return getLangJsonKey(jsonKey,curLang);
    }
    public static string getLangJsonKey(string  jsonKey, langs tLang){
        if(tLang==langs._ru) return jsonKey;
        else return jsonKey+tLang.ToString();
    }
   public void onButtonClik(int numLang){
        setLang((langs)numLang);
        JsonDB.me1.RefreshAll();
    }
   public static CultureInfo getCultureInfo(){
     if(curLang==langs._ru) return CultureInfo.CreateSpecificCulture("ru-RU");
     if(curLang==langs._en) return CultureInfo.CreateSpecificCulture("en-EN");
     if(curLang==langs._ar) return CultureInfo.CreateSpecificCulture("ar-AR");
     return CultureInfo.CreateSpecificCulture("ru-RU");
   }   
}
}
