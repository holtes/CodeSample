using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IconsController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject dtpIcon;
    public GameObject mmIcon;
    public GameObject veloIcon;
    private CameraController cameraController;
    public Animator veloRadial;

    private void Awake()
    {
        if (cameraController == null) cameraController = GameObject.Find("CameraRoot").GetComponent<CameraController>();
    }

    void Start()
    {
        
    }

    public void ShowVelo()
    {
        foreach (VeloIcon vi in transform.GetComponentsInChildren<VeloIcon>())
        {
            vi.GetComponent<Animator>().enabled = true;
        }
    }

    private void OnDisable()
    {
        foreach (VeloIcon vi in transform.GetComponentsInChildren<VeloIcon>())
        {
            vi.GetComponent<Animator>().Rebind();
            vi.GetComponent<Animator>().enabled = false;
            vi.transform.GetChild(0).localScale = Vector3.zero;
            vi.transform.GetChild(1).localScale = Vector3.zero;
        }
        veloRadial.Rebind();
    }

    public void UpdateIcons()
    {
        int newZoom = ZeroPointUI.me1.cameraController.currentZoomLevel;
        switch (newZoom)
        {
            case 0:
                transform.localScale = new Vector3(1, 1, 1);
                transform.Find("MM").gameObject.SetActive(false);
                ZeroPointUI.me1.hexPlaces.gameObject.SetActive(false);
                transform.Find("Velo").Find("0").gameObject.SetActive(true);
                transform.Find("Velo").Find("1").gameObject.SetActive(false);
                transform.localPosition = new Vector3(0, 132, 0);
                break;
            case 1:
                transform.Find("MM").gameObject.SetActive(false);
                ZeroPointUI.me1.hexPlaces.gameObject.SetActive(false);
                transform.Find("Velo").Find("0").gameObject.SetActive(true);
                transform.Find("Velo").Find("1").gameObject.SetActive(false);
                transform.localPosition = new Vector3(0, 132, 0);
                break;
            case 2:
                transform.Find("MM").gameObject.SetActive(false);
                ZeroPointUI.me1.hexPlaces.gameObject.SetActive(false);
                transform.Find("Velo").Find("0").gameObject.SetActive(false);
                transform.Find("Velo").Find("1").gameObject.SetActive(true);
                transform.localPosition = new Vector3(0, 32, 0);
                break;
            case 3:
                transform.localPosition = new Vector3(0, 5.7f, 0);
                transform.Find("Velo").Find("0").gameObject.SetActive(false);
                transform.Find("Velo").Find("1").gameObject.SetActive(true);
                if (!ZeroPointUI.me1.hexagonsController.gameObject.activeSelf)
                {
                    transform.Find("MM").gameObject.SetActive(true);
                }
                else
                {
                    ZeroPointUI.me1.hexPlaces.gameObject.SetActive(true);
                }
                break;
        }
        if (ZeroPointUI.me1.veloLayer.active)
        {
            transform.localPosition = new Vector3(0, 21.5f, 0);
        }
        for (int i = 1; i < transform.childCount; i++)
        {
            foreach (Place icon in transform.GetChild(i).GetComponentsInChildren<Place>())
            {
                if (icon.transform.parent.gameObject.name == "0" || icon.transform.parent.gameObject.name == "0")
                {
                    return;
                }
                switch (newZoom)
                {
                    case 0:
                        icon.transform.localScale = new Vector3(1, 1, 1);
                        break;
                    case 1:
                        icon.transform.localScale = new Vector3(0.68f, 0.68f, 0.68f);
                        break;
                    case 2:
                        icon.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
                        break;
                    case 3:
                        icon.transform.localScale = new Vector3(0.14f, 0.14f, 0.14f);
                        break;
                }
            }
        }
    }

    public void SetOkrug(string okrug)
    {
        foreach (Place icon in transform.GetComponentsInChildren<Place>())
        {
            if (icon.okrug == okrug.ToUpper() || icon.okrug == "" || ZeroPointUI.me1.cameraController.currentZoomLevel == 0)
            {
                icon.SetStatus(true);
                continue;
            }
            icon.SetStatus(false);
        }
    }

    public void ClearLayer(string layer)
    {
        foreach (Place icon in transform.Find(layer).GetComponentsInChildren<Place>())
        {
            Destroy(icon.gameObject);
        }
    }

    public void CreateDtp(float lat, float lon, Sprite icon, string okrug)
    {
        GameObject newIcon = Instantiate(dtpIcon);
        newIcon.transform.Find("Rotate").localEulerAngles = new Vector3(-60, 0, 0);
        newIcon.transform.SetParent(transform.Find("Trafic"));
        newIcon.GetComponentsInChildren<SpriteRenderer>()[1].sprite = icon;
        newIcon.transform.localPosition = ZeroPointUI.LatLon2XY(lat, lon);
        newIcon.GetComponent<Place>().okrug = okrug;
    }
    
    public Vector3 CreateVelo(float lat, float lon, string okrug, string color)
    {
        GameObject newIcon = Instantiate(veloIcon);
        newIcon.transform.Find("Rotate").localEulerAngles = new Vector3(330, 0, 0);
        newIcon.transform.SetParent(transform.Find("Velo").Find("1"));
        newIcon.transform.localPosition = ZeroPointUI.LatLon2XY(lat, lon);
        newIcon.GetComponent<Place>().okrug = okrug;
        Color parsedColor = new Color(0, 0, 0);
        ColorUtility.TryParseHtmlString("#" + color, out parsedColor);
        newIcon.GetComponent<Place>().SetColor(parsedColor);
        return newIcon.transform.position;
    }



    public void CreateMonument(float lat, float lon, string name, Sprite sprite)
    {
        GameObject newIcon = Instantiate(mmIcon);
        newIcon.name = name;
        newIcon.GetComponentInChildren<SpriteRenderer>().sprite = sprite;
        newIcon.transform.SetParent(transform.Find("MM"));
        newIcon.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        newIcon.transform.eulerAngles = new Vector3(-38, 0, 0);
        newIcon.transform.localPosition = ZeroPointUI.LatLon2XY(lat, lon);
        newIcon.GetComponentInChildren<TMP_Text>().text = name;
    }

    public void ChangeStatusMM(bool newStatus)
    {
        transform.Find("MM").gameObject.SetActive(newStatus);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
