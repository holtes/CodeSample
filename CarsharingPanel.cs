using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

public class CarsharingPanel : MonoBehaviour
{
    public Color color;
    public float strToFloat(string str)
    {
        return float.Parse(str, CultureInfo.InvariantCulture.NumberFormat);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnValidate()
    {
        if (ZeroPointUI.me1 == null)
        {
            return;
        }
        string jsonString = JsonDBProvider.getJsonString("Carsharing",2);
        T_Structure tData = JsonUtility.FromJson<T_Structure>(jsonString);
        foreach (Hex_Data hex in tData.Map)
        {
            ZeroPointUI.me1.hexagonsController.SetHex(hex.id, hex.value, color);
        }
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
        string jsonString = JsonDBProvider.getJsonString("Carsharing");
        T_Structure tData = JsonUtility.FromJson<T_Structure>(jsonString);
        ZeroPointUI.me1.titlePanel.setTitle(tData.header);
        foreach (Hex_Data hex in tData.Map)
        {
            ZeroPointUI.me1.hexagonsController.SetHex(hex.id, hex.value, color);
        }
        //ZeroPointUI.me1.hexagonsController.CollisionObject.move = true;
    }

    private void OnDisable()
    {
        ZeroPointUI.me1.hexagonsController.gameObject.SetActive(false);
        ZeroPointUI.me1.hexPlaces.gameObject.SetActive(false);
    }

    // Update is called once per frame
//    void Update(){}
}
