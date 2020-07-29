using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UIElements;

public class CameraMover : MonoBehaviour
{   
    [SerializeField]
    Transform TubesTransform = null;

    void Start()
    {
        Path path = TubesTransform.GetComponentInChildren<PathCreator>().path;
        if (path.NumSegments > 0)
            transform.LookAt(path[0]);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float rotateHorizontal = -Input.GetAxis("Mouse X");
        float rotateVertical = -Input.GetAxis("Mouse Y");
        if (true)//(Input.GetKey(KeyCode.LeftControl)) 
        {
            transform.RotateAround(transform.position, -Vector3.up, rotateHorizontal * 1f);
            transform.RotateAround(transform.position, transform.right, rotateVertical * 1f);
        }

        transform.position += GetBaseInput();
    }

    private Vector3 GetBaseInput()
    { 
        Vector3 p_Velocity = new Vector3(0.0f, 0.0f, 0.0f);
        if (Input.GetKey(KeyCode.W))
        {
            p_Velocity += transform.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            p_Velocity += -transform.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            p_Velocity += -transform.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            p_Velocity += transform.right;
        }
        if (Input.GetKey(KeyCode.Z))
        {
            p_Velocity += -transform.up;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            p_Velocity += transform.up;
        }

        return p_Velocity;
    }
}
