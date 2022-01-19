using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class MetroController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject station;
    public Hex scalableObject;
    [Header("Debug")]
    public string staticFileName;

    private void Awake()
    {

    }

    public void ClearAllColors()
    {
        foreach (Station st in transform.GetComponentsInChildren<Station>())
        {
            st.materials.Clear();
        }
    }

    public void loadStatic()
    {
        string jsonString = JsonDBProvider.getJsonString(staticFileName);
        JSONNode N = JSON.Parse(jsonString);
        foreach (JSONNode m in N.AsArray)
        {
            CreateStation(m["coordinates"]["lat"].AsFloat, m["coordinates"]["lon"].AsFloat, 2, m["id"].Value);
        }
    }

    void Start()
    {
        //Show();
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
        return worldCoordinates - ZeroPointUI.me1.transform.position;
    }

    public void CreateStation(float lat, float lon, int count, string id)
    {
        GameObject newStation = Instantiate(station);
        newStation.name = id;
        newStation.transform.SetParent(transform);
        newStation.transform.localScale = new Vector3(385.73f, 385.73f, 385.73f);
        newStation.transform.localPosition = PositionByLatLon2XY(lat, lon);
        newStation.GetComponent<Station>().SetStation(count, new Color(255, 255, 255));
        
    }

    public void Show()
    {
        scalableObject.move = true;
        StartCoroutine(WaitScalableObjectEndMove());
    }

    IEnumerator WaitScalableObjectEndMove()
    {
        while (scalableObject.percent <= 0.40f)
            yield return null;

        foreach (Station st in transform.GetComponentsInChildren<Station>())
        {
            st.makeOpaque = true;
            st.makeRealSize = true;
        }
        yield return null;
    }

    private void OnDisable()
    {
        scalableObject.Null();
        foreach (Station st in transform.GetComponentsInChildren<Station>())
        {
            st.SetStation(2, new Color(st.color_prev.r, st.color_prev.g, st.color_prev.b, 0), true);
            st.transform.localScale = new Vector3(st.transform.localScale.x, 0, st.transform.localScale.z);
        }
    }

    public void SetUnderStationMaterial()
    {
        foreach (Station st in GetComponentsInChildren<Station>())
        {
            st.transform.GetChild(3).GetComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            st.transform.GetChild(4).GetComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        }
    }

    public void SetStation(string id, int value, string color)
    {
        Transform st = transform.Find(id);
        if (st == null)
        {
            return;
        }
        
        int parsedValue = 0;
        //if (value < 2)
        //{
        //    parsedValue = 1;
        //}
        //else if (value < 4)
        //{
        //    parsedValue = 2;
        //}
        //else
        //{
        //    parsedValue = 3;
        //}
        Station station = st.GetComponent<Station>();
        Color parsedColor = new Color(0, 0, 0, 0);
        ColorUtility.TryParseHtmlString("#" + color, out parsedColor);
        parsedColor.a = 0;
        station.SetStation(parsedValue, parsedColor);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
