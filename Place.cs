using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Place : MonoBehaviour
{
    // Start is called before the first frame update
    public string okrug;
    void Start()
    {
        
    }

    public void SetColor(Color color)
    {
        transform.GetComponentInChildren<SpriteRenderer>().color = color;
    }

    public void SetStatus(bool newStatus)
    {
        foreach (SpriteRenderer sprite in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            sprite.enabled = newStatus;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
