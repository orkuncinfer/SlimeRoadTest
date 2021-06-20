using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    public Vector3 movingDirection;


    public GameObject platform1, platform2;
    private bool t = true;

    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        transform.Translate(movingDirection * Time.deltaTime);
    }
 
    public void Recycle()
    {
        if (t)
        {
            platform1.transform.position = platform2.transform.GetChild(0).transform.position;
            t = false;
        }else
        {
            platform2.transform.position = platform1.transform.GetChild(0).transform.position;
            t = true;
        }
        
    }
}
