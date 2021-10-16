using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    [SerializeField] float planetRotateSpeed = -25f;
    [SerializeField] float orbitSpeed = 30f;

    void Update()
    {
        transform.Rotate(transform.up * planetRotateSpeed * Time.deltaTime);
        transform.RotateAround(transform.parent.position, Vector3.up, orbitSpeed * Time.deltaTime);
    }
}
