using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VH.jsonDB;
using System.Globalization;

public class ZeroPointUI : MonoBehaviour
{
    public PanelTitleUI titlePanel;
    public HexagonsController hexagonsController; // инициализируеться в редакторе.
    public MainMenu mainMenu; // инициализируеться в редакторе.
    public MainMenuPanelUI mainMenuPanelUI; // инициализируеться в редакторе.
    public IconsController iconsController; // инициализируеться в редакторе.
    public GameObject veloLayer; // инициализируеться в редакторе.
    public GameObject traficLayer; // инициализируеться в редакторе.
    public GameObject mmLayer; // инициализируеться в редакторе.
    public MetroController metroController; // инициализируеться в редакторе.
    public MetroController mcdController; // инициализируеться в редакторе.
    public MatrixController matrixController; // инициализируеться в редакторе.
    public RiverController riverController; // инициализируеться в редакторе.
    public DistrictController districtController; // инициализируеться в редакторе.
    public NewParkingController newParkingController;
    public StaticController hexPlaces;
    public MetroController cppkController;
    public GameObject okrugData;
    public CameraController cameraController; // инициализируеться в редакторе.
    public GameObject dashboardScreen; // инициализируеться в редакторе.
    public GameObject welcomeScreen;
    public GameObject ngptGraphHigway;
    public GameObject ngptGraph;
    public static ZeroPointUI me1; // единственный экземпляр класса
    public ScreenItem[] screens;
    public float timeout;
    public int weatherIconIndex;
    private int current = -1;
    private ScreenPanels lastItType = ScreenPanels.Default;
    public SubMenu panelSub;
    public bool demoModeDisabled;
    public bool cameraInteractive = true;
    void Awake()
    {
       me1 = this;    
    }

    [System.Serializable]
    public class ScreenItem
    {
        public ScreenPanels screenPanel;
        public int submenuItemNumber;
    }
    public enum ScreenPanels { Default, RoadTrafic, UrbanTransport, SmartCrossroads }

    // Start is called before the first frame update
    // void Start(){}

    public void demoNextScreen()
    {
        if (currentItem() >= screens.Length)
        {
            timeout = JsonDB.generalConfig.demo_mode_start_timeout;
        }
        
        ScreenItem it = screens[currentItem()];
        
        if (it.screenPanel != lastItType)
        {
            switch (it.screenPanel)
            {
                case ScreenPanels.RoadTrafic:
                    mainMenuPanelUI.RoadTraficButtonPassive.onClick.Invoke();
                    panelSub = mainMenu.RoadTraficPanel.transform.Find("SubMenu").GetComponent<SubMenu>();
                    break;
                case ScreenPanels.SmartCrossroads:
                    mainMenuPanelUI.SmartCrossroadsButtonPassive.onClick.Invoke();
                    panelSub = mainMenu.SmartCrossroadsPanel.transform.Find("SubMenu").GetComponent<SubMenu>();
                    break;
                case ScreenPanels.UrbanTransport:
                    mainMenuPanelUI.UrbanTransportButtonPassive.onClick.Invoke();
                    panelSub = mainMenu.UrbanTransportPanel.transform.Find("SubMenu").GetComponent<SubMenu>();
                    break;
            }
        }
        lastItType = it.screenPanel;
        if (it.screenPanel != ScreenPanels.SmartCrossroads)
        {
            panelSub.subButtons[it.submenuItemNumber].GetComponent<Button>().onClick.Invoke();
        }
        //screenButtons[(int)(timeout / 15) - 1].onClick.Invoke();
        current = currentItem();
    }

    public static float strToFloat(string str, int indexLast = -1)
    {
        if (indexLast != -1)
        {
            str = str.Substring(0, str.Length - indexLast);
        }
        if (!str.Contains("%"))
        {
            if (isNumber(str))
            {
                return float.Parse(str, NumberStyles.Float, CultureInfo.InvariantCulture);
            }
        }
        try
        {
            return float.Parse(str.Substring(0, str.Length - 1), NumberStyles.Float, CultureInfo.InvariantCulture);
        }
        catch
        {
            return 0;
        }
    }

    public static bool isNumber(string str)
    {
        try
        {
            return float.TryParse(str, NumberStyles.Float, CultureInfo.InvariantCulture, out _);
        }
        catch
        {
            return false;
        }
    }

    public static Vector3 LatLon2XY(float lat, float lon, bool zeroLocal = true)
    {
        Vector3 delta = new Vector3(410495f, 0, 740945.78f);
        double deg2rad = Mathf.PI / 180;
        Vector3 data = new Vector3();
        lat = Mathf.Min(89.5f, Mathf.Max(lat, -89.5f));
        data.x = (float)(6378137 * lon * deg2rad);
        data.z = (float)(6378137 * Mathf.Log(Mathf.Tan((float)(Mathf.PI / 4f + lat * deg2rad / 2f))));
        data *= 0.1f;
        if (zeroLocal)
        {
            data -= delta;
        }
        return data;
    }
    
    public static Color strToColor(string str)
    {
        Color parsedColor = new Color(0, 0, 0);
        ColorUtility.TryParseHtmlString("#" + str, out parsedColor);
        return parsedColor;
    }

    public int currentItem()
    {
        return (int)((timeout - JsonDB.generalConfig.demo_mode_start_timeout) / JsonDB.generalConfig.demo_mode_step_timeout);
    }

    void Update()
    {
        if (demoModeDisabled)
        {
            return;
        }
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0 || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            timeout = 0;
            current = -1;
            lastItType = ScreenPanels.Default;
        }
        else
        {
            timeout += Time.deltaTime;
            if ((timeout >= JsonDB.generalConfig.demo_mode_start_timeout & currentItem() != current) || (Input.GetKeyDown(KeyCode.P) & current == -1))
            {
                if (Input.GetKeyDown(KeyCode.P))
                {
                    timeout = JsonDB.generalConfig.demo_mode_start_timeout;
                }
                cameraController.transform.localPosition = new Vector3(297.899994f, 0, 744);
                demoNextScreen();
            }
        }
    }
}
