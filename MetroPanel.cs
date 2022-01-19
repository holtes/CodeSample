using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

public class MetroPanel : MonoBehaviour
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
        ZeroPointUI.me1.metroController.gameObject.SetActive(true);
        string jsonString = JsonDBProvider.getJsonString("Metro", 2);
        MD_Structure mdData = JsonUtility.FromJson<MD_Structure>(jsonString);
        ZeroPointUI.me1.titlePanel.setTitle(mdData.header);
        foreach (MD_Map st in mdData.Map)
        {
            ZeroPointUI.me1.metroController.SetStation(st.id, st.value, st.color);

        }
        ZeroPointUI.me1.metroController.Show();
    }

    public void LoadStaticStations()
    {
        string jsonString = JsonDBProvider.getJsonString("Metro");
        M_Structure mdData = JsonUtility.FromJson<M_Structure>(jsonString);
        foreach (Metro m in mdData.Metro)
        {
            ZeroPointUI.me1.metroController.CreateStation(m.coordinates.lat, m.coordinates.lon, 2, m.id);
        }
    }

    private void OnDisable()
    {
        ZeroPointUI.me1.metroController.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
