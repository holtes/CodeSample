using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Vizart{
public class VLocaleUI : MonoBehaviour
{
    public GameObject ChgLangPanel;

    // Start is called before the first frame update
    //void Start(){}

    // Update is called once per frame
    //void Update(){}
    public void showChgLangPanel(bool isShow){
      ChgLangPanel.SetActive(isShow);
    }
    public void onShowBtn(){
        showChgLangPanel(!ChgLangPanel.activeSelf);
    }
    public void onLangBtn(int numLang){
        if(VLocale.me1==null) return;
        VLocale.me1.onButtonClik(numLang);
        showChgLangPanel(false);
    }
}
}
