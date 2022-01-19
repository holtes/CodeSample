using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class VeloIcon : MonoBehaviour
{
    public int currCount;
    public List<string> colors;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetIconRotationAndScale(Vector3 newRotation, Vector3 newScale)
    {
        Transform child = transform.Find("Rotate");
        child.localEulerAngles = newRotation;
        child.localScale = newScale;
    }

    public void UpdateCount(int count)
    {
        currCount = count;
        transform.GetComponentInChildren<TMP_Text>().text = count.ToString();
    }

    public void SetStatus(bool
        newStatus)
    {
        if (newStatus == true && currCount == 0)
        {
            return;
        }
        if (newStatus)
        {
            SetColor();
        }
        foreach (SpriteRenderer cmp in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            cmp.enabled = newStatus;
        }
        transform.GetChild(3).gameObject.SetActive(newStatus);
    }

    public void SetColor()
    {
        if (colors.Count == 0)
        {
            return;
        }
        string newColor = colors.OrderBy(x => colors.Count(y => y == x)).Last();
        Color parsedColor = new Color(0, 0, 0);
        ColorUtility.TryParseHtmlString("#" + newColor, out parsedColor);
        transform.GetComponentsInChildren<SpriteRenderer>()[0].color = parsedColor;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
