using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using System.Linq;
using System;

public class HexagonsController : MonoBehaviour
{
    public Material hexMaterial;
    private GameObject zeroPoint;
    public GameObject hexagon;
    public GameObject hexagonCenter;
    public Hex CollisionObject;
    public Vector3 startScale;
    public Vector3 endScale;
    public Dictionary<string, Material> hexsObjexts = new Dictionary<string, Material>();
    
    // Start is called before the first frame update

    private void Awake()
    {
        //if (zeroPoint == null) zeroPoint = GameObject.Find("ZeroPoint");
    }

    void Start()
    {
        
    }

    public void LoadMaterials()
    {
        foreach (MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
        {
            hexsObjexts[mr.transform.parent.gameObject.name] = mr.material;
        }
    }

    public void NullAll()
    {
        foreach (string m in hexsObjexts.Keys)
        {
            hexsObjexts[m].color = new Color(1, 1, 1, 0);
        }
    }

    public void SetTexture(Texture texture)
    {
        //hexMaterial.SetTexture("_BaseMap", texture);
    }

    public void SetHex(string id, int scale, Color newc)
    {
        if (hexsObjexts.Count < 10)
        {
            LoadMaterials();
        }
        if (!hexsObjexts.ContainsKey(id)) { return; }
        Material curMat = hexsObjexts[id];
        curMat.color = new Color(newc.r, newc.g, newc.b, Mathf.Lerp(0.03843138f, 0.5411765f, scale / 3f));
        //Transform trns = transform.Find(id);
        //if (trns == null) return;
        //Hex center = trns.GetChild(0).GetComponent<Hex>();
        //center.realSize = ((endScale - startScale) / 5 * scale);
    }

    public void LoadHexagonsStatic()
    {
        //string jsonString = JsonDBProvider.getJsonString("hexagons_grid");
        //H_Structure hData = JsonUtility.FromJson<H_Structure>(jsonString);
        //foreach (Hexagon hex in hData.hexagons)
        //{
        //    CreateHexagon(hex.id, hex.coordinates);
        //}
    }

    public void CreateCenters()
    {
        //string jsonString = JsonDBProvider.getJsonString("centroids_new");
        //HC_Structure hcData = JsonUtility.FromJson<HC_Structure>(jsonString);
        //foreach (Centroid hCenter in hcData.centroids)
        //{
        //    GameObject center = Instantiate(hexagonCenter);
        //    center.transform.position = ZeroLocalToWorld(PositionByLatLon2XY(hCenter.coordinates.lat, hCenter.coordinates.lon)) + new Vector3(0, transform.position.y, 0);
        //    center.transform.SetParent(transform.Find(hCenter.id.ToString()));
        //}
    }

    public Vector3 ZeroLocalToWorld(Vector3 zeroLocalCoordinates)
    {
        return zeroPoint.transform.position + zeroLocalCoordinates;
    }

    public float strToFloat(string str)
    {
        return float.Parse(str, CultureInfo.InvariantCulture.NumberFormat);
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

    public void CreateHexagon(string id, Coordinate[] coordinates)
    {
        //GameObject newHex = new GameObject();
        //newHex.AddComponent<LineRenderer>();
        //newHex.transform.SetParent(transform);
        //newHex.name = id;
        //LineRenderer lineRenderer = newHex.GetComponent<LineRenderer>();
        //lineRenderer.positionCount = 7;
        //lineRenderer.startWidth = 5;
        //lineRenderer.endWidth = 5;
        //for (int i = 0; i < coordinates.Length; i++)
        //{
        //    lineRenderer.SetPosition(i, PositionByLatLon2XY(strToFloat(coordinates[i].lat), strToFloat(coordinates[i].lon)) + new Vector3(0, 4.8f, 0));
        //}
    }

    private void OnDisable()
    {
        //NullAll();
    }

    private void OnEnable()
    {
        NullAll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
