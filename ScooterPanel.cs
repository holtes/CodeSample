using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using Vizart;

public class ScooterPanel : MonoBehaviour
{
    public Color color;
    public Sprite closedIcon;
    private void Awake()
    {

    }

    public float strToFloat(string str)
    {
        return float.Parse(str, CultureInfo.InvariantCulture.NumberFormat);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        ZeroPointUI.me1.hexagonsController.gameObject.SetActive(true);
        ZeroPointUI.me1.mmLayer.gameObject.SetActive(false);
        if (ZeroPointUI.me1.cameraController.currentZoomLevel == 3)
        {
            ZeroPointUI.me1.hexPlaces.gameObject.SetActive(true);
        }
        //ZeroPointUI.me1.hexagonsController.SetTexture(texture.texture);
        string jsonString = JsonDBProvider.getJsonString("Scooter", 2);
        T_Structure tData = JsonUtility.FromJson<T_Structure>(jsonString);
        ZeroPointUI.me1.titlePanel.setTitle(tData.header);
        //foreach (Hex_Data hex in tData.Map)
        //{
        //    ZeroPointUI.me1.hexagonsController.SetHex(hex.id, hex.value, color);
        //}
        PanelClosedUI.setImage(closedIcon);
        PanelClosedUI.setTitle(VLocale.me1.Translate("Сезон закрыт"));
        PanelClosedUI.SetStatus(true);
        //ZeroPointUI.me1.hexagonsController.CollisionObject.move = true;
    }

    private void OnDisable()
    {
        ZeroPointUI.me1.hexagonsController.gameObject.SetActive(false);
        ZeroPointUI.me1.hexPlaces.gameObject.SetActive(false);
        PanelClosedUI.SetStatus(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
