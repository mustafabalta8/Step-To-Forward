using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPos;

    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)] float movementFactor;
    [SerializeField] float period = 2f;

    const float tau = Mathf.PI * 2f; // constant value of 6.283 -> full circle
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; }// instead of 0 I used Mathf.Epsilon
        float cycles = Time.time / period; //continiously growing over time
        
        float rawSinWave = Mathf.Sin(cycles);//* tau//going from -1 to 1 
        movementFactor = (rawSinWave + 1f) / 2f; //recalculated to go from 0 to 1

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
}
