using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{

    [Header("Controls")]
    public int jumpForce = 1000;
    public float moveSpeed;
    [Header("Components to Assign")]
    public CharacterController CC;
    public Rigidbody rb;
    public Animator anim;
    public Platform platformSc;

    public static BallController instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        Physics.gravity = new Vector3(0, -40, 0);
        Time.timeScale = 0;
    }

    void Update()
    {

        SwipeContol();

    }

    private void SwipeContol()
    {
        StartTime();
        if (Input.GetMouseButton(0))
        {
            Plane plane = new Plane(Vector3.up, 0);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float distance;

            if (plane.Raycast(ray, out distance))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, ray.GetPoint(distance).z);
            }

            if (transform.position.z >= 2.42f) // Clamp Z axis
                transform.position = new Vector3(transform.position.x, transform.position.y, 2.42f);
            if(transform.position.z <= -2.42f)
                transform.position = new Vector3(transform.position.x, transform.position.y, -2.42f);
        }
   
    }

    private void Jump()
    {
        rb.velocity = Vector3.zero;
        rb.AddRelativeForce(Vector3.up * jumpForce);
        anim.SetBool("Grounded", true);
    }

    public void Die()
    {
        SceneManager.LoadScene(0);
    }

    private void StartTime()
    {
        if(Input.GetMouseButtonDown(0))
        Time.timeScale = 1;
    }

    private void OnTriggerEnter(Collider other)
    {

        switch (other.tag)
        {
            case "Ground":
                Jump(); break;
            case "Diamond":
                switch (other.transform.parent.tag)
                {
                    case "MovingRing":
                        RingsManager.instance.movingRings.Push(other.transform.parent.gameObject);
                        other.transform.parent.gameObject.SetActive(false);
                        RingsManager.instance.SpawnRings();
                        break;

                    case "DefaultRing":
                        RingsManager.instance.defaultRings.Push(other.transform.parent.gameObject);
                        other.transform.parent.gameObject.SetActive(false);
                        RingsManager.instance.SpawnRings();
                        break;
                } break;

            case "Ring":
                Die();break;
            case "platform":
                platformSc.Recycle(); break;
        }     
            
    }

    private void OnTriggerExit(Collider other)
    {
        anim.SetBool("Grounded", false);
    }



}
    
    


