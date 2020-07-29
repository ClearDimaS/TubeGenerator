using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SphMeshEditor : MonoBehaviour
{
    Mesh mesh;

    Vector3[] verticles;
    int[] triangles;

    float Radius;
    int num_steps;
    float start_phi;
    float stop_phi;
    SphMeshHelper meshHelper;

    public void UpdMesh(float _Radius, float _num_steps, float _start_phi, float _stop_phi)
    {
        Radius = _Radius - 0.3f;
        num_steps = Mathf.RoundToInt(_num_steps);
        start_phi = _start_phi;
        stop_phi = _stop_phi;
        meshHelper = new SphMeshHelper();

        mesh = new Mesh();
        GetComponentInChildren<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();
    }
    private void CreateShape()
    {
        verticles = getVerticles();

        triangles = getTriangles();
    }

    Vector3[,] globe;
    private Vector3[] getVerticles()
    {
        List<Vector3> res = new List<Vector3>();
        globe = new Vector3[num_steps + 2, num_steps];

        float r = Radius;

        float lat_step = (360.0f / (num_steps));
        float lon_step = 180.0f / (num_steps);

        for (int i = 0; i < num_steps + 2; i++)
        {
            for (int j = 0; j < num_steps; j++)
            {
                float x = r * Mathf.Sin((lon_step * j) * Mathf.Deg2Rad) * Mathf.Cos((( -stop_phi + lat_step * i)) * Mathf.Deg2Rad);
                float y = r * Mathf.Sin((lon_step * j) * Mathf.Deg2Rad) * Mathf.Sin(((-stop_phi + lat_step * i)) * Mathf.Deg2Rad);
                float z = r * Mathf.Cos((lon_step * j) * Mathf.Deg2Rad);

                res.Add(new Vector3(x, y, z));
                globe[i, j] = new Vector3(x, y, z);

            }
        }
        return res.ToArray();
    }

    private int[] getTriangles()
    {
        List<int> res = new List<int>();

        for (int i = 0; i < num_steps; i++)
        {
            for (int j = 0; j < num_steps; j++)
            {
                float x = globe[i, j].x; float y = globe[i, j].y; float z = globe[i, j].z;

                if (meshHelper.includeOrNot(start_phi, stop_phi, x, y, z))
                {
                    res.Add(i * num_steps + j); res.Add(i * num_steps + j + 1); res.Add((i + 1) * num_steps + j);

                    res.Add((i + 1) * num_steps + j); res.Add(i * num_steps + j + 1); res.Add((i + 1) * num_steps + j + 1); // for making visible from outside the tubes

                    res.Add(i * num_steps + j + 1); res.Add(i * num_steps + j);  res.Add((i + 1) * num_steps + j);

                    res.Add(i * num_steps + j + 1);  res.Add((i + 1) * num_steps + j); res.Add((i + 1) * num_steps + j + 1); // for making visible from inside the tubes
                }


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
