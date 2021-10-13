using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    Rigidbody myRigidbody;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        RotateRocket();
        ThrustRocket();
    }

    void RotateRocket()
    {
        if (Input.GetKey(KeyCode.A))
            Debug.Log("Left");

        else if (Input.GetKey(KeyCode.D))
            Debug.Log("Right");
    }

    void ThrustRocket()
    {
        if (Input.GetKey(KeyCode.Space))
            myRigidbody.AddRelativeForce(Vector3.up);
    }
}
