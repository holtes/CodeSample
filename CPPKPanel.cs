using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

public class CPPKPanel : MonoBehaviour
{
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
        ZeroPointUI.me1.cppkController.gameObject.SetActive(true);
        string jsonString = JsonDBProvider.getJsonString("CPPK", 2);
        CPPK_D_Structure cppkData = JsonUtility.FromJson<CPPK_D_Structure>(jsonString);
        ZeroPointUI.me1.titlePanel.setTitle(cppkData.header);
        foreach (CPPK_D_Station st in cppkData.Map)
        {
            ZeroPointUI.me1.cppkController.SetStation(st.id.ToString(), st.value, st.color);
        }
        ZeroPointUI.me1.cppkController.Show();
        //string jsonString = JsonDBProvider.getJsonString("CPPK_static");
        //Debug.Log(jsonString);
        //CPPK_Structure cppkData = JsonUtility.FromJson<CPPK_Structure>(jsonString);
        //foreach (CPPK_Station m in cppkData.CPPK)
        //{
        //    ZeroPointUI.me1.cppkController.CreateStation(m.coordinates.lat, m.coordinates.lon, 2, m.id.ToString());
        //}
    }

    private void OnDisable()
    {
        ZeroPointUI.me1.cppkController.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
