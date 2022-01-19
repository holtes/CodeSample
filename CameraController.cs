using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour {

    public delegate void ZoomChanged(int zoomLevel,float zoomVal);
    public static event ZoomChanged OnZoomChanged;

    public delegate void OkrugChanged(string okrug);
    public static event OkrugChanged OnOkrugChanged;

    public float smoothTime = 0.3f;
    private Camera mainCamera;
    public Transform m_Zoom;
    public SphereCollider cameraCollider;
    public Rect moveBounds;

    public float[] zoomLevels;

    public Button Minus_Btn;
    public Button Plus_Bth;

    private float m_DecelerationRate = 0.135f;

    private const string MoscowName = "Moscow";

    private Vector3 m_ZoomTarget;
    private int m_ZoomLevel;
    private Vector3 zoomVelocity = Vector3.zero; 
    private Vector3 startPos = Vector3.zero;
    private Vector3 delta = Vector3.zero;
    private Vector3 center = new Vector3(0.5f, 0.5f, 0);
    private Plane m_Plane;
    private bool m_Dragging;
    private Rigidbody cameraRigidbody;
    private Vector3 m_Velocity;
    private Vector3 m_LastPosition;
    private float radiusKoeff;
    public int currentZoomLevel;
    private MeshRenderer prevArea = null;
    private string prevOkrug = string.Empty;
    public string selectedOkrug;

    private readonly WaitForSeconds waitTime = new WaitForSeconds(0.3f);

    private int ZoomMaxLevel;

    public GameObject CameraMenuPanel;
    public static CameraController me1;
    private void Awake()
    {
        me1 = this;
    }
        void Start() {
        resetMaxZoomLevel();
        mainCamera = Camera.main;
        cameraRigidbody = GetComponent<Rigidbody>();

        m_LastPosition = transform.position;
        radiusKoeff = Mathf.Tan(mainCamera.fieldOfView * 0.5f * Mathf.Deg2Rad) * 2f;

        SetZoom(0);
        
        m_Plane = new Plane(Vector3.up * 10, Vector3.zero);
    }

    void Update() {
        UpdateControls();
        DetectOkrug();
    }
    private void OnEnable()
    {
        setCameraMenuPanel();
    }
    private void OnDisable()
    {
        CameraMenuPanel.SetActive(false);
    }
    public void setMaxZoomLevel(int newMaxZoomLevel)
    {
        if (newMaxZoomLevel < 0) return;
        if (newMaxZoomLevel >= zoomLevels.Length) return;
        ZoomMaxLevel = newMaxZoomLevel;
        if (currentZoomLevel > ZoomMaxLevel)
        {
            currentZoomLevel = ZoomMaxLevel;
            SetZoom(ZoomMaxLevel);
        }
        setCameraMenuPanel();
    }
    public void resetMaxZoomLevel()
    {
        ZoomMaxLevel = zoomLevels.Length-1;
        setCameraMenuPanel();
    }
private void setCameraMenuPanel()
    {
        CameraMenuPanel.SetActive(ZoomMaxLevel>0);
        setZoomButton();
    }
    void UpdateControls() {
        if (Input.GetMouseButtonDown(0) && !MouseOnUI)
        {
            startPos = GetPointOnGround(Input.mousePosition);
            m_Dragging = true;
        }// else m_Dragging = false;

        if (Input.GetMouseButtonUp(0))
        {
            m_Dragging = false;
        }
    }

    Vector3 GetPointOnGround(Vector3 mousePos)
    {
        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        float enter = 0.0f;
        if (m_Plane.Raycast(ray, out enter))
            return ray.GetPoint(enter);

        return Vector3.zero;
    }

    public void ZoomIn()
    {
        currentZoomLevel++;
        if (currentZoomLevel > ZoomMaxLevel) currentZoomLevel = ZoomMaxLevel;
        SetZoom(currentZoomLevel);
    }

    public void ZoomOut()
    {
        currentZoomLevel--;

        if (currentZoomLevel < 0)
            currentZoomLevel = 0;

        SetZoom(currentZoomLevel);
    }

    void SetZoom(int zoomLevel)
    {       
        m_ZoomLevel = zoomLevel;
        m_ZoomTarget.z = zoomLevels[zoomLevel];
        //cameraCollider.radius = m_Zoom.localPosition.z * radiusKoeff;
        setZoomButton();
        ZeroPointUI.me1.mainMenu.UpdateIcons();
        StopAllCoroutines();
        StartCoroutine(WaitForZoom());
    }
    void setZoomButton()
    {
        if (m_ZoomLevel == 0) Minus_Btn.gameObject.SetActive(false);
        else Minus_Btn.gameObject.SetActive(true);
        if(m_ZoomLevel>=ZoomMaxLevel) Plus_Bth.gameObject.SetActive(false);
        else Plus_Bth.gameObject.SetActive(true);
    }

    IEnumerator WaitForZoom()
    {
        yield return waitTime;
        OnZoomChanged?.Invoke(m_ZoomLevel, m_ZoomTarget.z);
    }

    void LateUpdate() {
        m_Zoom.localPosition = Vector3.SmoothDamp(m_Zoom.localPosition, m_ZoomTarget, ref zoomVelocity, smoothTime);

        float deltaTime = Time.unscaledDeltaTime;
        //Vector3 position = transform.position;
        if (!m_Dragging && m_Velocity != Vector3.zero)
        {
            for (int axis = 0; axis < 3; axis += 2)
            {
                m_Velocity[axis] *= Mathf.Pow(m_DecelerationRate, deltaTime);
                if (Mathf.Abs(m_Velocity[axis]) < 1)
                    m_Velocity[axis] = 0;
                //position[axis] += m_Velocity[axis] * deltaTime;
            }

            //SetPosition(position);
        }

        if (m_Dragging)
        {
            delta = startPos - GetPointOnGround(Input.mousePosition);

            m_Velocity = delta * 10;
            //Vector3 newVelocity = delta / deltaTime;
            //m_Velocity = Vector3.Lerp(m_Velocity, newVelocity, deltaTime * 10);

            //SetPosition(transform.position);
        }
        cameraRigidbody.velocity = m_Velocity;
    }

    void DetectOkrug()
    {
        if (Physics.Raycast(mainCamera.ViewportPointToRay(center), out RaycastHit okrug, Mathf.Infinity, 1 << 4))
        {
            selectedOkrug = okrug.collider.name;
            if (!prevOkrug.Equals(selectedOkrug))
            {
                if (prevArea != null)
                {
                    prevArea.enabled = false;
                }
                prevArea = okrug.collider.GetComponent<MeshRenderer>();
               // prevArea.enabled = true;

                prevOkrug = selectedOkrug;
                OnOkrugChanged?.Invoke(selectedOkrug);
            }
        }
        else
        {
            selectedOkrug = MoscowName;
            if (!prevOkrug.Equals(selectedOkrug))
            {
                if (prevArea != null)
                {
                    prevArea.enabled = false;
                    prevArea = null;
                }

                prevOkrug = selectedOkrug;
                OnOkrugChanged?.Invoke(selectedOkrug);
            }
        }
    }

    void SetPosition(Vector3 pos)
    {
        if (pos.x > moveBounds.width)
            pos.x = moveBounds.width;
        if (pos.z > moveBounds.height)
            pos.z = moveBounds.height;

        if (pos.x < moveBounds.xMin)
            pos.x = moveBounds.xMin;
        if (pos.z < moveBounds.yMin)
            pos.z = moveBounds.yMin;

        transform.position = pos;
    }

    public static bool MouseOnUI
    {
        get
        {
            //bool OnUI = EventSystem.current.IsPointerOverGameObject();
            bool OnUI = !ZeroPointUI.me1.cameraInteractive;
            if (Input.touchSupported)
            {
                if (Input.touchCount > 0)
                {
                    OnUI = EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
                    Debug.Log("touchCount on UI " +OnUI); 
                }
            } 
            if (OnUI) //else   //          
            {
                PointerEventData pointer = new PointerEventData(EventSystem.current);
                pointer.position = Input.mousePosition;
                List<RaycastResult> raycastResults = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointer, raycastResults);
                if (raycastResults.Count > 0)
                {
                    if (raycastResults[0].gameObject.layer == 0)
                        return false;
                }
            }

            return OnUI;
        }
    }
    public string getSelectedOkrug()
    {
        return selectedOkrug;
    }
}
