using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityMobilityIndexPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        //ZeroPointUI.me1.districtController.gameObject.SetActive(true);
        //string jsonString = JsonDBProvider.getJsonString("CityMobilityIndex");
        //CMI_Structure cmiData = JsonUtility.FromJson<CMI_Structure>(jsonString);
        //foreach (District district in cmiData.Map)
        //{
        //    ZeroPointUI.me1.districtController.SetDistrictData(district.name, district.value.ToString(), district.color);
        //}
        //ZeroPointUI.me1.districtController.ShowAllDisctricts();
    }

    private void OnDisable()
    {
        //ZeroPointUI.me1.districtController.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
