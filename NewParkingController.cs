using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewParkingController : MonoBehaviour
{
    // Start is called before the first frame update
    public string staticFileName;
    public GameObject spawnPrefab;
    public GameObject[] layers;
    private PS_R_Structure rayoniPoints = null;
    private PS_Structure zonePoints = null;

    private void Awake()
    {
        
    }

    void Start()
    {
        
    }

    public void SetShowLevel(int zm)
    {
        foreach (GameObject lvl in layers)
        {
            lvl.SetActive(false);
        }
        switch (zm)
        {
            case 0:
                layers[0].SetActive(true);
                break;
            case 1:
                layers[1].SetActive(true);
                for (int i = 0; i < layers[1].transform.childCount; i++)
                {   
                    layers[1].transform.GetChild(i).transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                }
                break;
            case 2:
                layers[1].SetActive(true);
                for (int i = 0; i < layers[1].transform.childCount; i++)
                {
                    layers[1].transform.GetChild(i).transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                }
                break;
            case 3:
                layers[2].SetActive(true);
                break;
            default:
                layers[1].SetActive(true);
                break;
        }
    }

    public void LoadStaticFiles()
    {
        zonePoints = JsonUtility.FromJson<PS_Structure>(JsonDBProvider.getJsonString("parking_zoom_3"));
        rayoniPoints = JsonUtility.FromJson<PS_R_Structure>(JsonDBProvider.getJsonString("parking_zoom_2"));
    }

    public void SetData(PD_Structure data)
    {
        if (zonePoints == null)
        {
            LoadStaticFiles();
        }
        foreach (PD_MapCity mapCity in data.MapCity)
        {
            Transform zone = layers[0].transform.Find(mapCity.name);
            if (zone == null)
            {
                continue;
            }
            zone.GetComponentInChildren<TMP_Text>().SetText(mapCity.value);
            float value = ZeroPointUI.strToFloat(mapCity.value.Substring(0, mapCity.value.Length - 1));
            if (value < 50)
            {
                zone.GetComponent<SpriteRenderer>().material.SetColor("_Color", new Color(0.2117647f, 0.454902f, 0));
            }
            else if (value < 85)
            {
                zone.GetComponent<SpriteRenderer>().material.SetColor("_Color", new Color(1, 0.9647059f, 0));
            }
            else
            {
                zone.GetComponent<SpriteRenderer>().material.SetColor("_Color", new Color(1, 0, 0));
            }
        }
        List<string> collected = new List<string>();
        Debug.Log(data.MapRayoni.Length);
        foreach (PD_MapRayoni mapRayoni in data.MapRayoni)
        {
            Color parsedColor = new Color(0, 0, 0);
            ColorUtility.TryParseHtmlString("#" + mapRayoni.color, out parsedColor);
            Transform obj = layers[1].transform.Find(mapRayoni.id.ToString());
            obj.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().color = parsedColor;
            
            collected.Add(mapRayoni.id.ToString());
        }
        foreach (PS_R_Data pd in rayoniPoints.Rayoni.data)
        {
            if (collected.Contains(pd.id.ToString())) { continue; };
            layers[1].transform.Find(pd.id.ToString()).gameObject.SetActive(false);
        }
        collected.Clear();
        foreach (PD_MapZone mapRayoni in data.MapZones)
        {
            Color parsedColor = new Color(0, 0, 0);
            ColorUtility.TryParseHtmlString("#" + mapRayoni.color, out parsedColor);
            Transform obj = layers[2].transform.Find(mapRayoni.id);
            obj.GetChild(0).GetChild(3).GetComponent<SpriteMask>().isCustomRangeActive = true;
            obj.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().color = parsedColor;
            obj.gameObject.SetActive(true);
            collected.Add(mapRayoni.id);
        }
        foreach (PS_Data pd in zonePoints.ParkingZones)
        {
            if (collected.Contains(pd.id.ToString())) { continue; };
            layers[2].transform.Find(pd.id).gameObject.SetActive(false);
        }
        collected.Clear();



    }
    public void Test()
    {
        string jsonString = JsonDBProvider.getJsonString("Parking");
        PD_Structure pData = JsonUtility.FromJson<PD_Structure>(jsonString);
        Debug.Log(pData.MapRayoni.Length);
    }

    public void LoadStatic()
    {
        string jsonSrc = JsonDBProvider.getJsonString(staticFileName);
        PS_R_Structure dt = JsonUtility.FromJson<PS_R_Structure>(jsonSrc);
        foreach (PS_R_Data item in dt.Rayoni.data)
        {
            GameObject newObj = Instantiate(spawnPrefab);
            newObj.transform.SetParent(transform.GetChild(1));
            newObj.transform.localPosition = ZeroPointUI.LatLon2XY(item.coordinates.lat, item.coordinates.lon, true);
            newObj.name = item.id.ToString();
        }
        //string jsonSrc = JsonDBProvider.getJsonString(staticFileName);
        //PS_Structure dt = JsonUtility.FromJson<PS_Structure>(jsonSrc);
        //foreach (PS_Data item in dt.ParkingZones)
        //{
        //    GameObject newObj = Instantiate(spawnPrefab);
        //    newObj.transform.SetParent(transform.GetChild(2));
        //    newObj.transform.localPosition = ZeroPointUI.LatLon2XY(ZeroPointUI.strToFloat(item.coordinates.lat), ZeroPointUI.strToFloat(item.coordinates.lon), true);
        //    //newObj.transform.localPosition = ZeroPointUI.LatLon2XY(item.coordinates.lat, item.coordinates.lon, true);
        //    newObj.name = item.id.ToString();
        //}

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
