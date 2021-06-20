using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingCollector : MonoBehaviour
{
    private int passedRings = 0;

    private void FixedUpdate()
    {
        if(passedRings >= 3)
        {
            BallController.instance.Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Diamond")
        {
            switch (other.transform.parent.tag)
            {
                case "MovingRing":
                    RingsManager.instance.movingRings.Push(other.transform.parent.gameObject);
                    other.transform.parent.gameObject.SetActive(false);
                    RingsManager.instance.SpawnRings();
                    passedRings++;
                    break;

                case "DefaultRing":
                    RingsManager.instance.defaultRings.Push(other.transform.parent.gameObject);
                    other.transform.parent.gameObject.SetActive(false);
                    RingsManager.instance.SpawnRings();
                    passedRings++;
                    break;

            }

        }
    }
}
