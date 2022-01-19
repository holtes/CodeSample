using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    // Start is called before the first frame update
    public bool moving;
    public float speed;
    public float progress;
    public Vector3 endPosition;
    public GameObject scaleUp;
    public GameObject scaleDown;
    public float distance;
    void Start()
    {
        
    }

    void Update()
    {
        distance = Vector3.Distance(transform.position, endPosition);
        if (!moving)
        {
            return;
        }
        if (transform.position != endPosition && distance > 50)
        {
            if (scaleUp.transform.localScale.x > -72)
            {
                scaleUp.transform.localScale = Vector3.MoveTowards(scaleUp.transform.localScale, new Vector3(-72, 72, 72), speed / 10 * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, endPosition, speed * Time.deltaTime);
            }
            return;
        }
        else if (scaleUp.active)
        {
            scaleUp.SetActive(false);
            scaleDown.SetActive(true);
        }
        if (scaleDown.transform.localScale.x > 1)
        {
            scaleDown.transform.localScale = Vector3.MoveTowards(scaleDown.transform.localScale, new Vector3(1, 72, 72), speed / 7 * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
