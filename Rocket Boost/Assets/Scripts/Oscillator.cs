using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0, 1)] float movementFactor;
    [SerializeField] float period = 20f;
    
    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        Oscillate();
    }

    void Oscillate()
    {
        float cycles = Time.time / period;          // Grows over time
        const float TAU = Mathf.PI * 2;             // Constant value of 6.283
        float rawSinWave = Mathf.Sin(cycles * TAU); // -1 to 1

        movementFactor = (rawSinWave + 1f) / 2f;    // Recalculated to go from 0 to 1.

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
