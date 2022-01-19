using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VH.jsonDB;
public class MainMenuPanelUI : MonoBehaviour
{
    [Header("Кнопки основного меню")]
    public Button RoadTraficButtonActive;
    public Button RoadTraficButtonPassive;
    public Button UrbanTransportButtonActive;
    public Button UrbanTransportButtonPassive;
    public Button SafetyEcologyButtonActive;
    public Button SafetyEcologyButtonPassive;
    public Button SmartCrossroadsButtonActive;
    public Button SmartCrossroadsButtonPassive;
    // Start is called before the first frame update
    void Start()
    {
        JsonDB.me1.LoadAll(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setActiveButton(MainMenuPanels selPanel)
    {
       // Debug.Log("setActiveButton: " +selPanel.ToString());
        RoadTraficButtonActive.gameObject.SetActive(false);
        RoadTraficButtonPassive.gameObject.SetActive(true);
        UrbanTransportButtonActive.gameObject.SetActive(false);
        UrbanTransportButtonPassive.gameObject.SetActive(true);
        //SafetyEcologyButtonActive.gameObject.SetActive(false);
        //SafetyEcologyButtonPassive.gameObject.SetActive(true);
        SmartCrossroadsButtonActive.gameObject.SetActive(false);
        SmartCrossroadsButtonPassive.gameObject.SetActive(true);
        switch (selPanel)
        {
            case MainMenuPanels.RoadTraficPanel:
                RoadTraficButtonActive.gameObject.SetActive(true);
                RoadTraficButtonPassive.gameObject.SetActive(false);
                break;
            case MainMenuPanels.UrbanTransportPanel:
                UrbanTransportButtonActive.gameObject.SetActive(true);
                UrbanTransportButtonPassive.gameObject.SetActive(false);
                break;
            case MainMenuPanels.SafetyEcologyPanel:
                SafetyEcologyButtonActive.gameObject.SetActive(true);
                SafetyEcologyButtonPassive.gameObject.SetActive(false);
                break;
            case MainMenuPanels.SmartCrossroadsPanel:
                SmartCrossroadsButtonActive.gameObject.SetActive(true);
                SmartCrossroadsButtonPassive.gameObject.SetActive(false);
                break;
        }
    }
}
