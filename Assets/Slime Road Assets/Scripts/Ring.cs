using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{

    public bool isMovingRing;
    public bool moveTo1 = false;

    public float moveSpeed;
    public float movePoint1;
    public float movePoint2;



    private void FixedUpdate()
    {
        if(isMovingRing)
        PatrolBetweenPoints();
    }


    private void PatrolBetweenPoints()
    {
        if (transform.position.z >= movePoint1)
        {
            moveTo1 = false;
        } 
        else if(transform.position.z <= movePoint2)
                {
            moveTo1 = true;
        }

        switch (moveTo1)
        {
            case true:
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, movePoint1), moveSpeed * Time.deltaTime);
                break;
            case false:
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, movePoint2), moveSpeed * Time.deltaTime);
                break;
        }
        
    }

}
