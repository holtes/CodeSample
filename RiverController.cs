using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RiverController : MonoBehaviour
{
    // Start is called before the first frame update
    private float percent = 0.23f;
    private bool percentDown;
    public float speed;
    public Material riverMaterial;

    [System.Serializable]
    public class RiverPoint
    {
        public GameObject gameObject;
        public IconTypes type;
    }
    public enum IconTypes { RiverStation, RiverPort, RiverPoint }

    public float timeout;
    public int showed = 0;

    public RiverPoint[] showOrder;

    void Start()
    {
        
    }

    private void OnDisable()
    {
        showed = 0;
        foreach (RiverPoint rp in showOrder)
        {
            foreach (Animator amtr in rp.gameObject.GetComponentsInChildren<Animator>())
            {
                amtr.Rebind();
                amtr.enabled = false;
            }
            switch (rp.type)
            {
                case IconTypes.RiverPoint:
                    rp.gameObject.transform.localScale = Vector3.zero;
                    break;
                default:
                    foreach (SpriteRenderer sr in rp.gameObject.GetComponentsInChildren<SpriteRenderer>())
                    {
                        sr.transform.localScale = Vector3.zero;
                    }
                    if (rp.type == IconTypes.RiverStation)
                    {
                        RectTransform rt = rp.gameObject.transform.Find("Rotate").Find("Title").GetComponent<RectTransform>();
                        rt.localPosition = new Vector3(rt.localPosition.x, rt.localPosition.y, 81);
                        rt.GetComponent<TMP_Text>().color = new Color(1, 1, 1, 0);
                    }
                    break;
            }
        }
    }

    public void ShowIcons()
    {
        for (int i = 0;i<showOrder.Length;i++)
        {
            StartCoroutine(showIcon(showOrder[i].gameObject, timeout * i));
        }
    }

    IEnumerator showIcon(GameObject gameObject, float to)
    {
        yield return new WaitForSeconds(to);
        foreach (Animator amt in gameObject.GetComponentsInChildren<Animator>())
        {
            amt.enabled = true;
        }
        showed += 1;
    }

    public void SetZoomLevel()
    {
        if (showed != showOrder.Length)
        {
            return;
        }
        int zoom = ZeroPointUI.me1.cameraController.currentZoomLevel;
        Vector3 newScale = new Vector3(0, 0, 0);
        switch (zoom)
        {
            case 0:
                newScale = new Vector3(2, 2, 2);
                break;
            case 1:
                newScale = new Vector3(1, 1, 1);
                break;
            case 2:
                newScale = new Vector3(0.58f, 0.58f, 0.58f);
                break;
            case 3:
                newScale = new Vector3(0.21f, 0.21f, 0.21f);
                break;

        }
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform trns = transform.GetChild(i);
            if (trns.name == "RiverTexture" || trns.name == "RadialDots")
            {
                continue;
            }
            trns.localScale = newScale;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!percentDown)
        {
            percent += speed * Time.deltaTime;
            if (percent >= 2.06f)
            {
                percentDown = true;
            }
        }
        else
        {
            percent -= speed * Time.deltaTime;
            if (percent <= 0.23f)
            {
                percentDown = false;
            }
        }
        riverMaterial.SetFloat("_offset", percent);
    }
}
