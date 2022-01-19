using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UrbanTransportPanelUI : MonoBehaviour
{
    public GameObject MatrixPanel;
    public GameObject MetroPanel;
    public GameObject TaxiPanel;
    public GameObject CarsharingPanel;
    public GameObject ScooterPanel;
    public GameObject MCDPanel;
    public GameObject VelorentPanel;
    public GameObject RiverPanel;
    public GameObject CPPKPanel;
    public GameObject NGPTPanel;

    public int selectMenuIndex=0;

    private bool isShowPanel = false;
    // Start is called before the first frame update
    void Start()
    {
    }
    private void OnEnable()
    {
        isShowPanel = false;        
    }
    // Update is called once per frame
    void Update(){
        if (!isShowPanel)
        {
            isShowPanel = true;
            showPanel(selectMenuIndex);
        }
    }

    public void onButtonClik(int MenuIndex)
    {
        // Debug.Log("onButtonClik - " + MenuIndex);
        string animPanelName = "";
        GameObject oldUI = null;
        GameObject panel = null;
        switch (selectMenuIndex)
        {
            case 0:
                panel = MatrixPanel;
                oldUI = ZeroPointUI.me1.okrugData;
                break;
            case 1:
                panel = MetroPanel;
                oldUI = panel.GetComponentInChildren<LeftBarPanelUI>().trafficLightLegenda.gameObject;
                break;
            case 2:
                panel = MCDPanel;
                oldUI = panel.GetComponentInChildren<LeftBarPanelUI>().trafficLightLegenda.gameObject;
                break;
            case 3:
                panel = CPPKPanel;
                oldUI = panel.GetComponentInChildren<LeftBarPanelUI>().trafficLightLegenda.gameObject;
                break;
            case 4:
                panel = NGPTPanel;
                break;
            case 5:
                panel = TaxiPanel;
                oldUI = panel.GetComponentInChildren<LeftBarPanelUI>().image2Legenda.gameObject;
                break;
            case 6:
                panel = CarsharingPanel;
                oldUI = panel.GetComponentInChildren<LeftBarPanelUI>().image2Legenda.gameObject;
                break;
            case 7:
                panel = RiverPanel;
                oldUI = panel.GetComponentInChildren<LeftBarPanelUI>().gradientBarLegenda.gameObject;
                break;
            case 8:
                panel = VelorentPanel;
                oldUI = panel.GetComponentInChildren<LeftBarPanelUI>().trafficLightLegenda.gameObject;
                break;
            case 9:
                panel = ScooterPanel;
                oldUI = panel.GetComponentInChildren<LeftBarPanelUI>().image2Legenda.gameObject;
                break;
        }
        
        GameObject fUI = null;
        if (oldUI != null)
        {
            fUI = Instantiate(oldUI, transform);
            fUI.GetComponent<RectTransform>().anchoredPosition = oldUI.GetComponent<RectTransform>().anchoredPosition;
            fUI.GetComponent<RectTransform>().sizeDelta = oldUI.GetComponent<RectTransform>().sizeDelta;
            fUI.GetComponent<RectTransform>().position = oldUI.GetComponent<RectTransform>().position;
            fUI.GetComponent<Animator>().SetBool("Hide", true);
        }
        if (selectMenuIndex == 8)
        {
            oldUI = panel.GetComponentInChildren<LeftBarPanelUI>().image2x2Legenda.gameObject;
            fUI = Instantiate(oldUI, transform);
            fUI.GetComponent<RectTransform>().anchoredPosition = oldUI.GetComponent<RectTransform>().anchoredPosition;
            fUI.GetComponent<RectTransform>().sizeDelta = oldUI.GetComponent<RectTransform>().sizeDelta;
            fUI.GetComponent<RectTransform>().position = oldUI.GetComponent<RectTransform>().position;
            fUI.GetComponent<Animator>().SetBool("Hide", true);
        }
        oldUI = panel.transform.Find("MultiDataViewPanel").gameObject;
        fUI = Instantiate(oldUI);
        fUI.transform.SetParent(transform);
        fUI.GetComponent<RectTransform>().position = oldUI.GetComponent<RectTransform>().position;
        fUI.GetComponent<RectTransform>().sizeDelta = oldUI.GetComponent<RectTransform>().sizeDelta;
        fUI.GetComponent<RectTransform>().localScale = oldUI.GetComponent<RectTransform>().localScale;
        fUI.transform.Find("MultiDataScrollView").GetComponent<Animator>().SetBool("Hide", true);
        showPanel(MenuIndex);
    }
    private void showPanel(int MenuIndex)
    {
        selectMenuIndex = MenuIndex;
        MatrixPanel.SetActive(false);
        MetroPanel.SetActive(false);
        TaxiPanel.SetActive(false);
        CarsharingPanel.SetActive(false);
        ScooterPanel.SetActive(false);
        MCDPanel.SetActive(false);
        VelorentPanel.SetActive(false);
        RiverPanel.SetActive(false);
        CPPKPanel.SetActive(false);
        NGPTPanel.SetActive(false);
        CameraController.me1.resetMaxZoomLevel();
        switch (MenuIndex)
        {
            case 0:
                MatrixPanel.SetActive(true);
                CameraController.me1.setMaxZoomLevel(1);
                break;
            case 1:
                MetroPanel.SetActive(true);
                CameraController.me1.setMaxZoomLevel(1);
                break;
            case 2:
                MCDPanel.SetActive(true);
                CameraController.me1.setMaxZoomLevel(1);
                break;
            case 3:
                CPPKPanel.SetActive(true);
                CameraController.me1.setMaxZoomLevel(1);
                break;
            case 4:
                NGPTPanel.SetActive(true);
                //CameraController.me1.setMaxZoomLevel(1);
                break;
            case 5:
                TaxiPanel.SetActive(true);
               // CameraController.me1.setMaxZoomLevel(1);
                break;
            case 6:
                CarsharingPanel.SetActive(true);
                //CameraController.me1.setMaxZoomLevel(1);
                break;
            case 7:
                RiverPanel.SetActive(true);
                CameraController.me1.setMaxZoomLevel(1);
                break;
            case 8:
                VelorentPanel.SetActive(true);
                //CameraController.me1.setMaxZoomLevel(1);
                break;
            case 9:
                ScooterPanel.SetActive(true);
                //CameraController.me1.setMaxZoomLevel(1);
                break;
            
        }
    }

}
