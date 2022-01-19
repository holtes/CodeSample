using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ParkingController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject parkingIcon;
    public GameObject zeroPoint;
    public string editLayer;
    public Vector3 newPosition;

    public void EditorUpdateIcons()
    {
        foreach (Icon icon in transform.GetComponentsInChildren<Icon>())
        {
            icon.UpdateCount(1000);
            icon.SetStatus(true);
            icon.GetComponentInChildren<TMP_Text>().color = Color.black;
            //icon.GetComponentInChildren<TMP_Text>().margin = new Vector4(-27.462944f, -14.7279243f, -27.8512821f, -15.2280064f);
            //icon.GetComponentInChildren<TMP_Text>().fontSizeMax = 309;
            //icon.GetComponentInChildren<TMP_Text>().horizontalAlignment = HorizontalAlignmentOptions.Left;
            //icon.GetComponentInChildren<TMP_Text>().verticalAlignment = VerticalAlignmentOptions.Middle;
            //icon.transform.GetChild(3).localPosition = newPosition;
        }
    }

    public void ClearStatic()
    {
        DestroyImmediate(transform.Find("1").gameObject);
        DestroyImmediate(transform.Find("2").gameObject);
    }

    public void LoadLevel(string level, Centroid[] centroids, float scale, float y)
    {
        GameObject lvl = transform.Find(level).gameObject;
        foreach (Centroid centr in centroids)
        {
            float lat, lon;
            if (centr.coordinates.lon > 49)
            {
                lon = centr.coordinates.lat;
                lat = centr.coordinates.lon;
            }
            else
            {
                lat = centr.coordinates.lat;
                lon = centr.coordinates.lon;
            }
            GameObject newIcon = Instantiate(parkingIcon);
            newIcon.name = centr.id;
            newIcon.transform.position = ZeroLocalToWorld(PositionByLatLon2XY(lat, lon)) + new Vector3(0, y, 0);
            newIcon.transform.SetParent(lvl.transform);
            newIcon.transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    public void LoadLevel(string level, HCL_Centroid[] centroids, float scale, float y)
    {
        GameObject lvl = transform.Find(level).gameObject;
        foreach (HCL_Centroid centr in centroids)
        {
            float lat, lon;
            if (centr.coordinates.lon > 49)
            {
                lon = centr.coordinates.lat;
                lat = centr.coordinates.lon;
            }
            else
            {
                lat = centr.coordinates.lat;
                lon = centr.coordinates.lon;
            }
            GameObject newIcon = Instantiate(parkingIcon);
            newIcon.name = centr.id.ToString();
            newIcon.transform.position = ZeroLocalToWorld(PositionByLatLon2XY(lat, lon)) + new Vector3(0, y, 0);
            newIcon.transform.SetParent(lvl.transform);
            newIcon.transform.localScale = new Vector3(scale, scale, scale);
        }
    }



    void Start()
    {

    }

    public void GenerateStatic()
    {
        string jsonString = JsonDBProvider.getJsonString("centroids");
        HC_Structure hcData = JsonUtility.FromJson<HC_Structure>(jsonString);
        GameObject level = new GameObject();
        level.name = "1";
        level.transform.SetParent(transform);
        level = new GameObject();
        level.transform.SetParent(transform);
        level.name = "2";

        
        jsonString = JsonDBProvider.getJsonString("centroids_late");
        HCL_Structure hclData = JsonUtility.FromJson<HCL_Structure>(jsonString);
        LoadLevel("1", hclData.centroids, 0.47f, 40);
        LoadLevel("2", hcData.centroids, 0.2f, 10);
    }

    public Vector3 ZeroLocalToWorld(Vector3 zeroLocalCoordinates)
    {
        return zeroPoint.transform.position + zeroLocalCoordinates;
    }

    public void NullAll()
    {
        foreach (Icon icon in transform.GetComponentsInChildren<Icon>())
        {
            icon.colors.Clear();
            icon.UpdateCount(0);
        }
    }

    public void ActivateAll()
    {
        gameObject.SetActiveRecursively(true);
    }

    public void SetStatusToAll(bool newStatus)
    {
        foreach (Icon icon in transform.GetComponentsInChildren<Icon>())
        {
            icon.SetStatus(newStatus);
        }
    }

    public void SetShowLevel(int newLevel)
    {
        switch (newLevel)
        {
            case 0:
            case 1:
                transform.Find("0").GetComponent<ParkingIconsLayer>().Enable();
                transform.Find("1").GetComponent<ParkingIconsLayer>().Disable();
                transform.Find("2").GetComponent<ParkingIconsLayer>().Disable();
                break;
            case 2:
                transform.Find("0").GetComponent<ParkingIconsLayer>().Disable();
                transform.Find("1").GetComponent<ParkingIconsLayer>().Enable();
                transform.Find("2").GetComponent<ParkingIconsLayer>().Disable();
                break;
            case 3:
                transform.Find("0").GetComponent<ParkingIconsLayer>().Disable();
                transform.Find("1").GetComponent<ParkingIconsLayer>().Disable();
                transform.Find("2").GetComponent<ParkingIconsLayer>().Enable();
                break;
        }
    }

    Vector3 delta = new Vector3(410495f, 0, 740945.78f);
    double deg2rad = Mathf.PI / 180;
    public Vector3 PositionByLatLon2XY(float lat, float lon)
    {
        Vector3 data = new Vector3();
        lat = Mathf.Min(89.5f, Mathf.Max(lat, -89.5f));
        data.x = (float)(6378137 * lon * deg2rad);
        data.z = (float)(6378137 * Mathf.Log(Mathf.Tan((float)(Mathf.PI / 4f + lat * deg2rad / 2f))));
        data *= 0.1f;
        data -= delta;
        return data;
    }

    public Vector3 WorldToZeroLocal(Vector3 worldCoordinates)
    {
        return worldCoordinates - zeroPoint.transform.position;
    }

    public void CreateParking(float lat, float lon, string districtName, string color)
    {
        Transform district = transform.Find("0").Find(districtName);
        Vector3 newPosition = PositionByLatLon2XY(lat, lon);
        Icon districtIcon = district.GetComponent<Icon>();
        districtIcon.colors.Add(color);
        //districtIcon.count += 1;
        //districtIcon.UpdateCount();
        for (int layer = 1; layer < 3; layer++)
        {
            Transform iclayer = transform.Find(layer.ToString());
            float minDistanceIc = 9999999999;
            Icon minIconIc1 = null;
            for (int i = 0; i < iclayer.childCount; i++)
            {
                Transform ic1 = iclayer.GetChild(i);
                float newDistance = Vector3.Distance(WorldToZeroLocal(ic1.position), newPosition);
                if (newDistance < minDistanceIc)
                {
                    minDistanceIc = newDistance;
                    minIconIc1 = ic1.GetComponent<Icon>();
                }
            }
            minIconIc1.colors.Add(color);
            //minIconIc1.count += 1;
            //minIconIc1.UpdateCount();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
