using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

public class MCDPanel : MonoBehaviour
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
        ZeroPointUI.me1.mcdController.gameObject.SetActive(true);
        string jsonString = JsonDBProvider.getJsonString("MCD", 2);
        MCD_Dynamical_Structure mcdData = JsonUtility.FromJson<MCD_Dynamical_Structure>(jsonString);
        ZeroPointUI.me1.titlePanel.setTitle(mcdData.header);
        foreach (MCD_D_Station st in mcdData.Map)
        {
            ZeroPointUI.me1.mcdController.SetStation(st.id.ToString(), st.value, st.color);
        }
        ZeroPointUI.me1.mcdController.Show();
        //string jsonString = JsonDBProvider.getJsonString("MCD_static");
        //MCD_Structure mcdData = JsonUtility.FromJson<MCD_Structure>(jsonString);
        //foreach (MCD_Station m in mcdData.MCD)
        //{
        //    ZeroPointUI.me1.mcdController.CreateStation(m.coordinates.lat, m.coordinates.lon, 2, (int)strToFloat(m.id));
        //}
    }

    private void OnDisable()
    {
        ZeroPointUI.me1.mcdController.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
