using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour
{
    public float Speed;
    public float SpeedMult;
    public float MaxOffset;
    float offset;
    Rigidbody2D Road;
    // Use this for initialization
    void Start()
    {
        Road = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Road.velocity = new Vector2(0, Speed);
        if (transform.position.y < -5)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
        }

        if (transform.position.y > 5)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 5, transform.position.z);
        }

        //Speed = Speed - 0.1f * Input.GetAxis("Vertical");
        //Debug.Log(Input.GetAxis("Vertical"));
        //offset = Mathf.Repeat(Time.time * SpeedMult * -Speed, MaxOffset);
        //GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0, offset);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector3(-15, 0), new Vector3(-15, offset));
        Gizmos.DrawLine(new Vector3(-16, 0), new Vector3(-16, Speed));

    }
    void XUpdate()
    {
    }
}
