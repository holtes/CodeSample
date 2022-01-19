using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class Icon : MonoBehaviour
{
    public int currCount;
    public List<string> colors;
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = -18;
        transform.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = -18;
        transform.GetChild(3).GetComponent<SpriteRenderer>().sortingOrder = -11;
    }

    public void SetIconRotationAndScale(Vector3 newRotation, Vector3 newScale)
    {
        for (int i = 0;i<4;i++)
        {
            Transform child = transform.GetChild(i);
            child.localEulerAngles = newRotation;
            child.localScale = newScale;
        }
    }

    public void UpdateCount(int count)
    {
        currCount = count;
        transform.GetChild(3).GetComponentInChildren<TMP_Text>().text = count.ToString();
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
        transform.GetComponentsInChildren<SpriteRenderer>()[0].enabled = newStatus;
        transform.GetComponentsInChildren<SpriteRenderer>()[1].enabled = newStatus;
        transform.GetComponentsInChildren<SpriteRenderer>()[2].enabled = newStatus;
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
