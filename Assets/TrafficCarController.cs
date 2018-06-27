using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficCarController : MonoBehaviour
{
    public float Speed;
    public float Acceleration;
    public float TopSpeed;
    public float MaxSpeed;
    public float MinSpeed;
    public float Threshold;
    public RoadController Road;
    private Rigidbody2D Car;
    CarState State;
    private Vector3 CollisionX;

    // Use this for initialization
    void Start()
    {
        MaxSpeed = Random.Range(MinSpeed, TopSpeed);
        Road = GameObject.Find("Road").GetComponent<RoadController>();
        Car = GetComponent<Rigidbody2D>();
        State = CarState.Accel;
        var skins = GameObject.Find("CarSkins").GetComponent<SkinsHolder>().Skins;
        GetComponent<SpriteRenderer>().sprite = skins[Random.Range(0, skins.Length)];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 net = new Vector3(0, 0, 0);
        foreach (var other in Others)
        {
            var otherv = other.GetComponent<Collider2D>().bounds.ClosestPoint(transform.position) - transform.position;
            if (otherv.y > 0.2)
            {
                net += other.GetComponent<Collider2D>().bounds.ClosestPoint(transform.position) - transform.position;
            }
        }
        Debug.Log(net);
        if (Mathf.Abs(net.x) * 0.1f > Threshold && net.y > 1)
            Car.AddForce(new Vector2(-Mathf.Sign(net.x) * Mathf.Lerp(200, 0, Mathf.Abs(net.x) * 0.01f), 0));
        if (net.y < Threshold)
        {
            State = CarState.Accel;
        }
        else if (net.y > Threshold)
        {
            State = CarState.DeAccel;
        }
        else
        {
            State = CarState.Accel;
        }
        switch (State)
        {
            case CarState.Accel:
                Speed = Speed + Acceleration * Time.deltaTime >= MaxSpeed ? MaxSpeed : Speed + Acceleration * Time.deltaTime;
                break;
            case CarState.Cruise:
                break;
            case CarState.DeAccel:
                Speed = Speed - Acceleration * Time.deltaTime >= 0 ? Speed - Acceleration * Time.deltaTime : 0; ;
                break;
            default:
                break;
        }
        Car.velocity = new Vector2(0, Road.Speed + Speed);
    }

    private List<GameObject> Others = new List<GameObject>();
    void OnTriggerEnter2D(Collider2D other)
    {
        Others.Add(other.gameObject);
        //State = CarState.DeAccel;
    }


    void OnTriggerExit2D(Collider2D other)
    {
        Others.Remove(other.gameObject);
        if (Others.Count == 0)
        {
            State = CarState.Accel;
        }
        //State = CarState.Accel;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {



    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position + new Vector3(1, 0, 0), transform.position + new Vector3(1, Speed, 0));

        Gizmos.color = Color.red;
        Vector3 net = transform.position;
        foreach (var other in Others)
        {
            Gizmos.DrawLine(other.GetComponent<Collider2D>().bounds.ClosestPoint(transform.position), transform.position);
            net += other.GetComponent<Collider2D>().bounds.ClosestPoint(transform.position) - transform.position;
        }
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(net.x, transform.position.y, transform.position.z), transform.position);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector3(transform.position.x, net.y, transform.position.z), transform.position);
        Gizmos.color = Color.white;
    }

}
