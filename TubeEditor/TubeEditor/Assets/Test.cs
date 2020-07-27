using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Test : MonoBehaviour
{
    Mesh mesh;

    Vector3[] verticles;
    int[] triangles;
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();
    }
    private void CreateShape()
    {
        verticles = getVerticles(10, 20, Mathf.PI / 2.0f);

        triangles = getTriangles(20);

        //verticles = new Vector3[]
        //{
        //    new Vector3(-1f, 1f, 0),
        //    new Vector3(-1f, 0f, 1),
        //    new Vector3(-1f, -1f, 0),
        //    new Vector3(-1f, 0f, -1),
        //    new Vector3(0f, 1f, 0),
        //    new Vector3(0f, 0f, 1),
        //    new Vector3(0f, -1f, 0),
        //    new Vector3(0f, 0f, -1),
        //    new Vector3(1f, 1f, 0),
        //    new Vector3(1f, 0f, 1),
        //    new Vector3(1f, -1f, 0),
        //    new Vector3(1f, 0f, -1),
        //};

        //triangles = new int[]
        //{
        //    0, 1, 2,
        //    2, 3, 0,
        //    2, 3, 4,
        //    5, 5, 2,
        //    0, 1, 2,
        //    2, 3, 0,
        //    0, 1, 2,
        //    2, 3, 0,
        //};
    }

    void Update()
    {
    }

    private Vector3[] getVerticles(float Radius, float num_steps, float phi)
    {
        List<Vector3> res = new List<Vector3>();

        float r = 200;
        for (int i = 0; i < num_steps + 2; i++)
        {
            float lat_step = 360.0f / num_steps;
            for (int j = 0; j < num_steps + 1; j++)
            {
                float lon_step = 180.0f / num_steps;
                float x = r * Mathf.Sin((lon_step * j) * Mathf.Deg2Rad) * Mathf.Cos((lat_step * i) * Mathf.Deg2Rad);
                float y = r * Mathf.Sin((lon_step * j) * Mathf.Deg2Rad) * Mathf.Sin((lat_step * i) * Mathf.Deg2Rad);
                float z = r * Mathf.Cos((lon_step * j) * Mathf.Deg2Rad);
                res.Add(new Vector3(x, y, z));
            }
        }

        return res.ToArray();
    }

    private int[] getTriangles(float _num_steps)
    {
        List<int> res = new List<int>();

        int num_steps = (int)_num_steps;

        for (int i = 0; i < num_steps + 1; i++)
        {
            for (int j = 0; j < num_steps; j++)
            {
                res.Add(i * num_steps + j); res.Add(i * num_steps + j + 1); res.Add((i + 1) * num_steps + j);

                res.Add((i + 1) * num_steps + j); res.Add(i * num_steps + j + 1); res.Add((i + 1) * num_steps + j + 1);
            }
        }

        return res.ToArray();
    }

    private void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = verticles;
        mesh.triangles = triangles;
    }
}
