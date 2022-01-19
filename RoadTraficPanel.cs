using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class RoadTraficPanel : MonoBehaviour
{
    private int count;
    public Sprite roadWorks;
    public Sprite accidents;

    public void Awake()
    {

    }

    void Start()
    {

    }

    public float strToFloat(string str)
    {
        return float.Parse(str, CultureInfo.InvariantCulture.NumberFormat);
    }

    public void OnEnable()
    {
        ZeroPointUI.me1.iconsController.gameObject.SetActive(true);
        ZeroPointUI.me1.iconsController.transform.Find("Trafic").gameObject.SetActive(true);
        ZeroPointUI.me1.iconsController.ClearLayer("Trafic");
        string jsonString = JsonDBProvider.getJsonString("RoadTransportSituation");
        if (jsonString == null || jsonString == "") return;
        RTS_Structure rtsData = JsonUtility.FromJson<RTS_Structure>(jsonString);
        foreach (Coordinates coordinates in rtsData.Accidents.coordinates)
        {
            foreach (Coordinate coordinate in coordinates.values)
            {
                ZeroPointUI.me1.iconsController.CreateDtp(strToFloat(coordinate.lat), strToFloat(coordinate.lon), accidents, coordinates.name);
            }
        }
        foreach (Coordinates coordinates in rtsData.RoadWorks.coordinates)
        {
            foreach (Coordinate coordinate in coordinates.values)
            {
                ZeroPointUI.me1.iconsController.CreateDtp(strToFloat(coordinate.lat), strToFloat(coordinate.lon), roadWorks, coordinates.name);
            }
        }
        ZeroPointUI.me1.iconsController.UpdateIcons();
    }

    public void UpdateIcons()
    {
        int newZoom = ZeroPointUI.me1.cameraController.currentZoomLevel;
        switch (newZoom)
        {
            case 0:
            case 1:
                ZeroPointUI.me1.iconsController.transform.localPosition = new Vector3(0, 132, 0);
                break;
            case 2:
                ZeroPointUI.me1.iconsController.transform.localPosition = new Vector3(0, 32, 0);
                break;
            case 3:
                ZeroPointUI.me1.iconsController.transform.localPosition = new Vector3(0, 5.7f, 0);
                break;
        }
        foreach (Place icon in ZeroPointUI.me1.iconsController.transform.GetChild(1).GetComponentsInChildren<Place>())
        {
            switch (newZoom)
            {
                case 0:
                case 1:
                    icon.transform.localScale = new Vector3(1, 1, 1);
                    break;
                case 2:
                    icon.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    break;
                case 3:
                    icon.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                    break;
            }
        }
    }

    public void OnDisable()
    {
        ZeroPointUI.me1.iconsController.transform.Find("Trafic").gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
