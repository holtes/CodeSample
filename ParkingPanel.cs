using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

class Icon_Data
{
    public int value { get; set; }
    public List<string> colors { get; set; }
}

public class ParkingPanel : MonoBehaviour
{
    // Start is called before the first frame update
    private HC_Structure hcData;
    private HCL_Structure hclData;
    public void UpdateIcons()
    {
        int zoom = ZeroPointUI.me1.cameraController.currentZoomLevel;
        //ZeroPointUI.me1.parkingController.SetShowLevel(zoom);
    }

    private void OnEnable()
    {
        string jsonString = JsonDBProvider.getJsonString("Parking", 2);
        PD_Structure pData = JsonUtility.FromJson<PD_Structure>(jsonString);
        ZeroPointUI.me1.titlePanel.setTitle(pData.header);
        ZeroPointUI.me1.newParkingController.SetData(pData);
        ZeroPointUI.me1.newParkingController.gameObject.SetActive(true);
        //Debug.Log("Конец цикла" + System.DateTime.Now.ToString());

    }

    private void OnDisable()
    {
        ZeroPointUI.me1.newParkingController.gameObject.SetActive(false);
    }

    void Start()
    {
        //string jsonString = JsonDBProvider.getJsonString("centroids");
        //hcData = JsonUtility.FromJson<HC_Structure>(jsonString);
        //jsonString = JsonDBProvider.getJsonString("centroids_late");
        //hclData = JsonUtility.FromJson<HCL_Structure>(jsonString);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
