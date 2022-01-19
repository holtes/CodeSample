using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using System.Linq;
using Vizart;

public class Velo_Data
{
    public int value { get; set; }
    public List<string> colors { get; set; }
}

public class Velo_Contact
{
    public Vector3 coordinate { get; set; }
    public VeloIcon obj { get; set; }
}



public class VelorentPanel : MonoBehaviour
{
    public List<Velo_Contact> vcData = new List<Velo_Contact>();
    public Sprite closedIcon;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void LoadVCData()
    {
        ZeroPointUI.me1.iconsController.transform.Find("Velo").Find("0").gameObject.SetActive(true);
        foreach (Transform trns in ZeroPointUI.me1.iconsController.transform.Find("Velo").Find("0"))
        {
            vcData.Add(new Velo_Contact { coordinate = trns.position, obj = trns.GetComponent<VeloIcon>() });
        }
    }

    private void OnEnable()
    {
        //return;
        //ZeroPointUI.me1.iconsController.gameObject.SetActive(true);
        //ZeroPointUI.me1.veloLayer.SetActive(true);
        //foreach (VeloIcon vi in ZeroPointUI.me1.veloLayer.GetComponentsInChildren<VeloIcon>())
        //{
        //    vi.GetComponent<Animator>().Rebind();
        //    vi.GetComponent<Animator>().enabled = false;
        //    vi.transform.GetChild(0).localScale = Vector3.zero;
        //    vi.transform.GetChild(1).localScale = Vector3.zero;
        //}
        //ZeroPointUI.me1.iconsController.veloRadial.Rebind();
        //if (vcData.Count == 0) LoadVCData();
        //ZeroPointUI.me1.iconsController.UpdateIcons();
        //foreach (Place pc in ZeroPointUI.me1.veloLayer.transform.Find("1").GetComponentsInChildren<Place>())
        //{
        //    Destroy(pc.gameObject);
        //}
        //string jsonString = JsonDBProvider.getJsonString("Velorent");
        //V_Structure vData = JsonUtility.FromJson<V_Structure>(jsonString);
        //ZeroPointUI.me1.titlePanel.setTitle(vData.header);
        //Dictionary<Velo_Contact, Velo_Data> result = new Dictionary<Velo_Contact, Velo_Data>();
        //foreach (V_Data data in vData.Map)
        //{
        //    Vector3 newPos = ZeroPointUI.me1.iconsController.CreateVelo(ZeroPointUI.strToFloat(data.coordinates.lat), ZeroPointUI.strToFloat(data.coordinates.lon), "", data.color);
        //    Velo_Contact vc = vcData.OrderBy(x => Vector3.Distance(newPos, x.coordinate)).First();
        //    if (!result.ContainsKey(vc))
        //    {
        //        result[vc] = new Velo_Data { value = 0, colors = new List<string>() };
        //    }
        //    result[vc].value += 1;
        //    result[vc].colors.Add(data.color);
        //}
        //foreach (Velo_Contact vc in result.Keys)
        //{
        //    vc.obj.UpdateCount(result[vc].value);
        //    vc.obj.colors = result[vc].colors;
        //    vc.obj.SetColor();
        //}
        PanelClosedUI.setImage(closedIcon);
        PanelClosedUI.SetStatus(true);
        PanelClosedUI.setTitle(VLocale.me1.Translate("Сезон закрыт"));
    }

    private void OnDisable()
    {
        ZeroPointUI.me1.veloLayer.SetActive(false);
        PanelClosedUI.SetStatus(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
