using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MainMenuPanels
{
    RoadTraficPanel, UrbanTransportPanel,SafetyEcologyPanel,SmartCrossroadsPanel, VideoScreenPanel
} 
public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuPanel;
    [Header("Переключаемые панели")]
    public MainMenuPanels currentShowPanel = MainMenuPanels.RoadTraficPanel;
    public GameObject RoadTraficPanel;
    public GameObject ParkingPanel;
    public GameObject RoadMovingPanel;
    public GameObject MMPanel;
    public GameObject UrbanTransportPanel;
    public GameObject VideoScreenPanel;
    public GameObject WelcomeScreenPanel;
    public GameObject SafetyEcologyPanel;
    public GameObject SmartCrossroadsPanel;
    [Header("Камерый")]
    public CameraController cameraController;
    public GameObject CinimaBackGroung;

    [HideInInspector]
    public static MainMenu me1; // возможен только один эземпляр этого касса на экране

    private int currentZoomLevel;
    private float currentZoomVal;
    private string currentSelectedOkrug;
    private bool isShowPanel = false;
    private void Awake()
    {
        me1 = this;
        CameraController.OnOkrugChanged += OkrugChanged;
        CameraController.OnZoomChanged += OnZoomChanged;
        if (MainMenuPanel == null) MainMenuPanel = GameObject.Find("MainMenuPanel");
        if (RoadTraficPanel==null) RoadTraficPanel=GameObject.Find("RoadTraficPanel");
        if (ParkingPanel==null) ParkingPanel=GameObject.Find("ParkingPanel");
        if (UrbanTransportPanel == null) UrbanTransportPanel = GameObject.Find("UrbanTransportPanel");
        if(SafetyEcologyPanel == null) SafetyEcologyPanel = GameObject.Find("SafetyEcologyPanel");
        if(SmartCrossroadsPanel == null) SmartCrossroadsPanel = GameObject.Find("SmartCrossroadsPanel");
        if(MMPanel == null) MMPanel = GameObject.Find("MonumentsMemorialsPanel");
        if(cameraController == null) cameraController = GameObject.Find("CameraRoot").GetComponent<CameraController>();
        if (CinimaBackGroung == null) CinimaBackGroung = GameObject.Find("CinimaBackGroung");
    }

    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
   void Update(){
        if(!isShowPanel)
        {
            isShowPanel = true;
            showPanels(currentShowPanel);
        }
    }

    void OnDestroy()
    {
        CameraController.OnOkrugChanged -= OkrugChanged;
        CameraController.OnZoomChanged -= OnZoomChanged;
    }


    public void UpdateIcons()
    {
        ZeroPointUI.me1.iconsController.UpdateIcons();
        ZeroPointUI.me1.riverController.SetZoomLevel();
        if (RoadMovingPanel.active)
        {
            RoadMovingPanel.GetComponent<RoadMovingParser>().UpdateZoomLevel();
        }
        ZeroPointUI.me1.newParkingController.SetShowLevel(ZeroPointUI.me1.cameraController.currentZoomLevel);
    }

    public void OnRoadTraficButton(){ showPanels(MainMenuPanels.RoadTraficPanel); }
    public void OnUrbanTransportButton() { showPanels(MainMenuPanels.UrbanTransportPanel); }
    public void OnSafetyEcologyButton() { showPanels(MainMenuPanels.SafetyEcologyPanel); }
    public void OnSmartCrossroadsButton() { showPanels(MainMenuPanels.SmartCrossroadsPanel); }
    public void showPanels(MainMenuPanels newPanels)
    {       
        currentShowPanel = newPanels;
        RoadTraficPanel.SetActive(false);
        UrbanTransportPanel.SetActive(false);
        SafetyEcologyPanel.SetActive(false);
        SmartCrossroadsPanel.SetActive(false);
        if(CinimaBackGroung!=null) CinimaBackGroung.SetActive(false);
        switch (newPanels)
        {
            case MainMenuPanels.RoadTraficPanel:
                ZeroPointUI.me1.cameraController.gameObject.SetActive(true);
                RoadTraficPanel.SetActive(true);
                break;
            case MainMenuPanels.UrbanTransportPanel:
                ZeroPointUI.me1.cameraController.gameObject.SetActive(true);
                UrbanTransportPanel.SetActive(true);
                break;
            case MainMenuPanels.SafetyEcologyPanel:
                ZeroPointUI.me1.cameraController.gameObject.SetActive(true);
                SafetyEcologyPanel.SetActive(true);
                break;
            case MainMenuPanels.SmartCrossroadsPanel:
                ZeroPointUI.me1.cameraController.gameObject.SetActive(false);
                if (CinimaBackGroung != null) CinimaBackGroung.SetActive(true);
                SmartCrossroadsPanel.SetActive(true);
                break;
            case MainMenuPanels.VideoScreenPanel:
                VideoScreenPanel.SetActive(true);
                break;
        }
        MainMenuPanel.GetComponent<MainMenuPanelUI>().setActiveButton(newPanels);
        OnZoomChanged(currentZoomLevel,currentZoomVal);
        OkrugChanged(currentSelectedOkrug);
    }


    private LevelChangeAbc getLevelChange()
    {
        return null;
        LevelChangeAbc ret=null;
        switch (currentShowPanel)
        {
            case MainMenuPanels.RoadTraficPanel:
                ret = RoadTraficPanel.GetComponent<LevelChangeAbc>();
                break;
            case MainMenuPanels.UrbanTransportPanel:
                ret = UrbanTransportPanel.GetComponent<LevelChangeAbc>();
                break;
            case MainMenuPanels.SafetyEcologyPanel:
                ret = SafetyEcologyPanel.GetComponent<LevelChangeAbc>();
                break;
            case MainMenuPanels.SmartCrossroadsPanel:
                ret=SmartCrossroadsPanel.GetComponent<LevelChangeAbc>();
                break;
        }
        return ret;
    }
    //Метод  вызывается каждый раз при изменении уровня от камеры.
    // zoomLevel - зум камеры
    void OnZoomChanged(int zoomLevel, float zoomVal)
    {
        currentZoomVal = zoomVal;
        currentZoomLevel = zoomLevel;
        LevelChangeAbc curLevelChange = getLevelChange();
        if (curLevelChange == null) return;
        curLevelChange.setZoom(zoomLevel,zoomVal);
    }

    //Метод  вызывается  каждый раз при изменении от камеры - на какой округ она смотрит.
    // selectedOkrug - имя выбранного округа
    void OkrugChanged(string selectedOkrug)
    {
        currentSelectedOkrug = selectedOkrug;
        LevelChangeAbc curLevelChange = getLevelChange();
        if (selectedOkrug != null)
        {
            if (ZeroPointUI.me1.iconsController.gameObject.active)
            {
                ZeroPointUI.me1.iconsController.SetOkrug(selectedOkrug);
            }
            if (ZeroPointUI.me1.matrixController.gameObject.active)
            {
                ZeroPointUI.me1.matrixController.SetDistrict(selectedOkrug.ToUpper());
            }
        }
        if (curLevelChange == null) return;
        curLevelChange.setOkrug(selectedOkrug);
        
    }
}
