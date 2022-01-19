using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

public class EcoUDSPanel : MonoBehaviour
{
    public Sprite texture;
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
        ZeroPointUI.me1.hexagonsController.SetTexture(texture.texture);
        string jsonString = JsonDBProvider.getJsonString("EcoUDS");
        T_Structure tData = JsonUtility.FromJson<T_Structure>(jsonString);
        //foreach (Hex_Data hex in tData.Map)
        //{
        //    ZeroPointUI.me1.hexagonsController.SetHex(hex.id, hex.value);
        //}
    }

    private void OnDisable()
    {
        ZeroPointUI.me1.hexagonsController.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
