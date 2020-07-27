using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshEditor : MonoBehaviour
{
    Mesh mesh;

    Vector3[] verticles;
    int[] triangles;

    float outter_R;
    float num_steps;
    float inner_R;
    float height;

    public void UpdMesh(float _outter_R, float _num_steps, float _inner_R, float _height)
    {
        num_steps = _num_steps;
        inner_R = _inner_R;
        outter_R = _outter_R;
        height = _height / 2.0f;
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();
    }

    private void CreateShape()
    {
        verticles = getVerticles();

        triangles = getTriangles();
    }

    private Vector3[] getVerticles() 
    {
        List<Vector3> res = new List<Vector3>();

        float alpha_step = 360.0f / num_steps;

        for (int i = 0; i < num_steps; i++)
        {
            res.Add(new Vector3(inner_R * Mathf.Cos(alpha_step * Mathf.Deg2Rad * i), inner_R * Mathf.Sin(alpha_step * Mathf.Deg2Rad * i), -height));

            res.Add(new Vector3(outter_R * Mathf.Cos(alpha_step * Mathf.Deg2Rad * i), outter_R * Mathf.Sin(alpha_step * Mathf.Deg2Rad * i), -height));
        }

        for (int i = 0; i < num_steps; i++) 
        {
            res.Add(new Vector3(inner_R * Mathf.Cos(alpha_step * Mathf.Deg2Rad * i), inner_R * Mathf.Sin(alpha_step * Mathf.Deg2Rad * i), height));

            res.Add(new Vector3(outter_R * Mathf.Cos(alpha_step * Mathf.Deg2Rad * i), outter_R * Mathf.Sin(alpha_step * Mathf.Deg2Rad * i), height));
        }

        return res.ToArray();
    }

    private int[] getTriangles() 
    {
        List<int> res = new List<int>();

        int mod = (int)num_steps * 2;

        for (int i = 0; i < num_steps; i++) 
        {
            res.Add(2 * i + 0); res.Add((2 * i + 3) % (mod )); res.Add(2 * i + 1);

            res.Add(2 * i + 0); res.Add((2 * i + 2) % (mod )); res.Add((2 * i + 3) % (mod));
        }

        for (int i = 0; i < num_steps; i++)
        {
            res.Add(2 * i + 0 + mod ); res.Add(2 * i + 1 + mod ); res.Add((2 * i + 3) % (mod ) + mod ); 

            res.Add(2 * i + 0 + mod ); res.Add((2 * i + 3) % (mod ) + mod ); res.Add((2 * i + 2) % (mod ) + mod ); 
        }

        for (int i = 0; i < num_steps; i++)
        {
            res.Add(2 * i + 0); res.Add((2 * i + 2) % (mod ) + mod ); res.Add((2 * i + 2) % (mod ));

            res.Add((2 * i + 2) % (mod ) + mod ); res.Add((2 * i + 0)); res.Add((2 * i + 0) + mod );
        }

        for (int i = 0; i < num_steps; i++)
        {
            res.Add(2 * i + 1); res.Add((2 * i + 3) % (mod )); res.Add((2 * i + 3) % (mod ) + mod );

            res.Add((2 * i + 3) % (mod ) + mod ); res.Add((2 * i + 1) + mod ); res.Add((2 * i + 1));
        }

        return res.ToArray();
    }

    private void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = verticles;
        mesh.triangles = triangles;
    }

    #region HardCoded values
    // This was done in order to get a better feeling of how triangles are rendered

    //verticles = new Vector3[]
    //{
    //    new Vector3(1f, 0, 0),
    //    new Vector3(2f, 0, 0),
    //    new Vector3(0, 1f, 0),
    //    new Vector3(0, 2f, 0),
    //    new Vector3(-1f, 0, 0),
    //    new Vector3(-2f, 0, 0),
    //    new Vector3(0, -1f, 0),
    //    new Vector3(0, -2f, 0),

    //    new Vector3(1f, 0, 10),
    //    new Vector3(2f, 0, 10),
    //    new Vector3(0, 1f, 10),
    //    new Vector3(0, 2f, 10),
    //    new Vector3(-1f, 0, 10),
    //    new Vector3(-2f, 0, 10),
    //    new Vector3(0, -1f, 10),
    //    new Vector3(0, -2f, 10),
    //};

    //triangles = new int[]
    //{
    //    0, 3, 1,  // bot
    //    0, 2, 3,
    //    2, 5, 3,
    //    2, 4, 5,
    //    4, 7, 5,
    //    4, 6, 7,
    //    6, 1, 7,
    //    6, 0, 1,

    //    8, 11, 9,  // top
    //    8, 10, 11,
    //    10, 13, 11,
    //    10, 12, 13,
    //    12, 15, 13,
    //    12, 14, 15,
    //    14, 9, 15,
    //    14, 8, 9,

    //    0, 10, 2, //inner
    //    10, 0, 8,
    //    2, 12, 4,
    //    12, 2, 10,
    //    4, 14, 6,
    //    14, 4, 12,
    //    6, 8, 0,
    //    8, 6, 14,

    //    1, 3, 11, //out
    //    11, 9, 1,
    //    3, 5, 13,
    //    13, 11, 3,
    //    5, 7, 15,
    //    15, 13, 5,
    //    7, 1, 9,
    //    9, 15, 7,
    //};
    #endregion
}
