using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Vizart{
[System.Serializable]
public class VLocaleControlItem 
{
    public GameObject CtlObj; // контролируемый объект
    public VLocale.langs lang=VLocale.langs.none;
    [HideInInspector]
    public string key;
    [HideInInspector]
    public bool isInit=false;
    public string resourseFileName;
}
}
