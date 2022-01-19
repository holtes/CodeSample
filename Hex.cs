using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour
{
    public Vector3 realSize;
    public bool move;
    public float percent;
    public float plus = 5f;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void Null()
    {
        percent = 0;
        move = false;
        transform.localScale = new Vector3(0, 0, 0);
    }

    public IEnumerator MoveStep()
    {
        if (percent < 1)
        {
            
           
            yield return new WaitForSeconds(0.1f / 60f);
            yield return MoveStep();
        }
        else
        {
            move = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (realSize != transform.localScale & percent < 1 & move)
        {
            percent += Time.deltaTime * plus;
            transform.localScale = Vector3.Lerp(new Vector3(0, 0, 0), realSize, percent);
            if (percent >= 1)
            {
                percent = 0;
                move = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (realSize != new Vector3(0, 0, 0) & !move & percent == 0f & collision.gameObject.name == "CollisionObject")
        {
            move = true;
        }
    }
}
