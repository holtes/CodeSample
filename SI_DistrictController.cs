using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SI_DistrictController : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {

    }

    public void SetDistrictData(string districtName, string newNumber, string newColor)
    {
        GameObject district = GameObject.Find(districtName);
        Material districtMaterial = district.GetComponent<MeshRenderer>().material;
        Color parsedColor = new Color(0, 0, 0);
        ColorUtility.TryParseHtmlString("#" + newColor, out parsedColor);
        districtMaterial.SetColor("d_color", parsedColor);
        district.transform.GetComponentsInChildren<SpriteRenderer>()[1].color = parsedColor;
        district.transform.GetComponentsInChildren<SpriteRenderer>()[2].color = parsedColor;
        TMP_Text securityIndex = district.GetComponentsInChildren<TMP_Text>()[0];
        securityIndex.text = newNumber;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
