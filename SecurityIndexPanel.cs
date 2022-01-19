using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityIndexPanel : MonoBehaviour
{
    // Start is called before the first frame 
    void Start()
    {
        
    }

    private void OnEnable()
    {
        ZeroPointUI.me1.districtController.gameObject.SetActive(true);
        string jsonString = JsonDBProvider.getJsonString("SecurityIndex");
        SI_Structure siData = JsonUtility.FromJson<SI_Structure>(jsonString);
        foreach (SI_MapData district in siData.Map)
        {
            ZeroPointUI.me1.districtController.SetDistrictData(district.name, district.value, district.color);
        }
    }

    private void OnDisable()
    {
        ZeroPointUI.me1.districtController.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
