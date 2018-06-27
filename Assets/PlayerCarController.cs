using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarController : MonoBehaviour
{

    public float Speed;
    public float Acceleration;
    public float MaxSpeed = 4f;
    public float MinSpeed = -2f;
    private Rigidbody2D Car;
    public RoadController Road;

    public Vector3 XYZ;
    // Use this for initialization
    void Start()
    {
        Car = GetComponent<Rigidbody2D>();
        Road = GameObject.Find("Road").GetComponent<RoadController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Car.velocity = new Vector2(0, Road.Speed + Speed);
    }

    void FixedUpdate()
    {
        Acceleration = 0;
        Acceleration += Input.GetAxis("Vertical");
        //Debug.Log(Acceleration);
        if (Acceleration >= 0.3)
        {
            Acceleration = 0.1f;
        }
        else if (Acceleration < -0.3)
        {
            Acceleration = -0.1f;
        }

        if (Speed + Acceleration > MaxSpeed)
        {
            Speed = MaxSpeed;
        }
        else if ((Speed + Acceleration) <= MaxSpeed && (Speed + Acceleration) >= MinSpeed)
        {
            Speed += Acceleration;
        }
        else
        {
            Speed = MinSpeed;
        }
        //Car.velocity = new Vector2(0, Road.Speed + Speed);
        Road.Speed = -Speed;
        Car.AddForce(new Vector2(Input.GetAxis("Horizontal") * 60f, 0));
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(XYZ, new Vector3(1, 1, 1));
    }
}
