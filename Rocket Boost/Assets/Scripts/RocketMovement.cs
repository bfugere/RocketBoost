using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 200f;

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
            ApplyRotation(rotationThrust);

        else if (Input.GetKey(KeyCode.D))
            ApplyRotation(-rotationThrust);
    }

    void ThrustRocket()
    {
        if (Input.GetKey(KeyCode.Space))
            myRigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
    }

    void ApplyRotation(float rotationThisFrame)
    {
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
    }
}
