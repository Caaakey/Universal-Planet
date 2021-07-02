using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlanet : MonoBehaviour
{
    public Vector3 RotateAxis = Vector3.up;
    public float RotateSpeed = 10.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(RotateAxis, RotateSpeed * Time.deltaTime);
    }
}
