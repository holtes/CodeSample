using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using Vizart;

public class RiverPanel : MonoBehaviour
{
    public Sprite closedIcon;
    private void OnEnable()
    {
        PanelClosedUI.setImage(closedIcon);
        PanelClosedUI.SetStatus(true);
        PanelClosedUI.setTitle(VLocale.me1.Translate("Навигация закрыта"));
        //string jsonSrc = JsonDBProvider.getJsonString("RiverTransport", 2);
        //if (jsonSrc != "")
        //{
        //    JSONNode N = JSON.Parse(jsonSrc);
        //    ZeroPointUI.me1.titlePanel.setTitle(N["header"].Value);
        //}
        //ZeroPointUI.me1.riverController.gameObject.SetActive(true);
        //ZeroPointUI.me1.riverController.SetZoomLevel();
    }

    private void OnDisable()
    {
        PanelClosedUI.SetStatus(false);
        //ZeroPointUI.me1.riverController.gameObject.SetActive(false);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
