using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshEditor : MonoBehaviour
{
    Mesh mesh;

    Vector3[] verticles;
    int[] triangles;
    Vector2[] uv;
    Vector3[] normals;

    float outter_R;
    int num_steps;
    float inner_R;
    float height;

    MeshMaterialHandler matHandler;
    [SerializeField]
    Texture text = null;

    float alpha;
    float alpha1;
    float add;
    public void UpdMesh(float _outter_R, float _num_steps, float _inner_R, float _height)
    {
        num_steps = Mathf.RoundToInt(_num_steps);
        if (num_steps % 2 == 0)
            num_steps += 1;
        inner_R = _inner_R;
        outter_R = _outter_R;
        height = _height / 2.0f;

        matHandler = new MeshMaterialHandler();

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();
    }

    private void CreateShape()
    {
        verticles = getVerticles();

        triangles = getTriangles();

        uv = getUvs(text.width, text.height);

        normals = getNormals();
    }

    private Vector3[] getVerticles() 
    {

        List<Vector3> res = new List<Vector3>();

        float alpha_step = 360.0f / (num_steps);

        for (int i = 0; i < num_steps + 1; i++)
        {
            res.Add(new Vector3(inner_R * Mathf.Cos(alpha_step * Mathf.Deg2Rad * i), inner_R * Mathf.Sin(alpha_step * Mathf.Deg2Rad * i), -(height)));//  + inner_R * Mathf.Tan(alpha + add) * Mathf.Sin(Mathf.PI + alpha_step * Mathf.Deg2Rad * i))

            res.Add(new Vector3(outter_R * Mathf.Cos(alpha_step * Mathf.Deg2Rad * i), outter_R * Mathf.Sin(alpha_step * Mathf.Deg2Rad * i), -(height)));
        }

        for (int i = 0; i < num_steps + 1; i++)
        {
            res.Add(new Vector3(outter_R * Mathf.Cos(alpha_step * Mathf.Deg2Rad * i), outter_R * Mathf.Sin(alpha_step * Mathf.Deg2Rad * i), -(height)));

            res.Add(new Vector3(outter_R * Mathf.Cos(alpha_step * Mathf.Deg2Rad * i), outter_R * Mathf.Sin(alpha_step * Mathf.Deg2Rad * i), (height)));
        }

        for (int i = 0; i < num_steps + 1; i++)
        {
            res.Add(new Vector3(inner_R * Mathf.Cos(alpha_step * Mathf.Deg2Rad * i), inner_R * Mathf.Sin(alpha_step * Mathf.Deg2Rad * i), (height)));

            res.Add(new Vector3(outter_R * Mathf.Cos(alpha_step * Mathf.Deg2Rad * i), outter_R * Mathf.Sin(alpha_step * Mathf.Deg2Rad * i), (height)));
        }

        for (int i = 0; i < num_steps + 1; i++)
        {
            res.Add(new Vector3(inner_R * Mathf.Cos(alpha_step * Mathf.Deg2Rad * i), inner_R * Mathf.Sin(alpha_step * Mathf.Deg2Rad * i), -(height)));

            res.Add(new Vector3(inner_R * Mathf.Cos(alpha_step * Mathf.Deg2Rad * i), inner_R * Mathf.Sin(alpha_step * Mathf.Deg2Rad * i), (height)));
        }

        return res.ToArray();
    }

    private int[] getTriangles() 
    {
        List<int> res = new List<int>();

        int offset = 0;

        for (int i = 0; i < num_steps; i++)
        {
            res.Add(2 * i + 1); res.Add(2 * i + 0); res.Add((2 * i + 3));//bottom

            res.Add((2 * i + 3)); res.Add(2 * i + 0); res.Add((2 * i + 2));
        }

        offset = (num_steps * 2 + 2) * 1;

        for (int i = 0; i < num_steps; i++)
        {
            res.Add(offset + 2 * i + 0); res.Add((offset + 2 * i + 3)); res.Add(offset + 2 * i + 1); //bottom

            res.Add(offset + 2 * i + 0); res.Add((offset + 2 * i + 2)); res.Add((offset + 2 * i + 3));
        }
        offset = (num_steps * 2 + 2) * 2;
        for (int i = 0; i < num_steps; i++)
        {
            res.Add(offset + 2 * i + 0); res.Add((offset + 2 * i + 1)); res.Add(offset + 2 * i + 3); //bottom

            res.Add(offset + 2 * i + 0); res.Add((offset + 2 * i + 3)); res.Add((offset + 2 * i + 2));
        }
        offset = (num_steps * 2 + 2) * 3;
        for (int i = 0; i < num_steps; i++)
        {
            res.Add(offset + 2 * i + 0); res.Add((offset + 2 * i + 1)); res.Add(offset + 2 * i + 3); //bottom

            res.Add(offset + 2 * i + 0); res.Add((offset + 2 * i + 3)); res.Add((offset + 2 * i + 2));
        }

        return (res).ToArray();
    }

    private Vector2[] getUvs(int textureWidth, int textureHeight)
    {
        matHandler = new MeshMaterialHandler();
        return matHandler.GetUVRectangleFromPixels(num_steps, textureWidth, textureHeight);
    }

    private Vector3[] getNormals()
    {
        return matHandler.GetNormals(verticles, num_steps);
    }

    private void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = verticles;
        mesh.triangles = triangles;
        mesh.uv = uv;
        mesh.normals = normals;
    }

    #region HardCoded values
    // This was done in order to get a better feeling of how triangles are rendered

//    verticles = new Vector3[]
//        {
//            new Vector3(1f, 0, 0), // bottom
//            new Vector3(2f, 0, 0),
//            new Vector3(0, 1f, 0),
//            new Vector3(0, 2f, 0),
//            new Vector3(-1f, 0, 0),
//            new Vector3(-2f, 0, 0),
//            new Vector3(0, -1f, 0),
//            new Vector3(0, -2f, 0),
//            new Vector3(1f, 0, 0), // bottom
//            new Vector3(2f, 0, 0),

//            new Vector3(2f, 0, 0), // outter
//            new Vector3(2f, 0, 10),
//            new Vector3(0, 2f, 0),
//            new Vector3(0, 2f, 10),
//            new Vector3(-2f, 0, 0),
//            new Vector3(-2f, 0, 10),
//            new Vector3(0, -2f, 0),
//            new Vector3(0, -2f, 10),
//            new Vector3(2f, 0, 0), // outter
//            new Vector3(2f, 0, 10),

//            new Vector3(1f, 0, 10), // top
//            new Vector3(2f, 0, 10),
//            new Vector3(0, 1f, 10),
//            new Vector3(0, 2f, 10),
//            new Vector3(-1f, 0, 10),
//            new Vector3(-2f, 0, 10),
//            new Vector3(0, -1f, 10),
//            new Vector3(0, -2f, 10),
//            new Vector3(1f, 0, 10), // top
//            new Vector3(2f, 0, 10),

//            new Vector3(1f, 0, 00),  // inner
//            new Vector3(1f, 0, 10),
//            new Vector3(0, 1f, 00),
//            new Vector3(0, 1f, 10),
//            new Vector3(-1f, 0, 00),
//            new Vector3(-1f, 0, 10),
//            new Vector3(0, -1f, 00),
//            new Vector3(0, -1f, 10),
//            new Vector3(1f, 0, 00),  // inner
//            new Vector3(1f, 0, 10),


//        };

//triangles = new int[]
//        {
//            0, 3, 1,  // bot
//            0, 2, 3,
//            2, 5, 3,
//            2, 4, 5,
//            4, 7, 5,
//            4, 6, 7,
//            6, 9, 7,
//            6, 8, 9,

//            10, 13, 11,  // outter
//            10, 12, 13,
//            12, 15, 13,
//            12, 14, 15,
//            14, 17, 15,
//            14, 16, 17,
//            16, 19, 17,
//            16, 18, 19,

//            20, 21,23,   // top
//            20,23, 22, 
//            22, 23,25, 
//            22,25, 24, 
//            24,25, 27, 
//            24, 27,26, 
//            26, 27,29, 
//            26, 29,28, 

//            30, 31,33,   // inner
//            30,33, 32, 
//            32, 33,35, 
//            32, 35,34, 
//            34, 35,37, 
//            34,37, 36, 
//            36, 37,39, 
//            36, 39,38, 

//            //8, 10, 9, //out
//            //10, 11, 9,
//            //10, 12, 11,
//            //12, 13, 11,
//            //12, 10, 9,
//            //14, 11, 9,
//            //14, 10, 9,
//            //16, 11, 9,


//            //8, 11, 9,  // top
//            //8, 10, 11,
//            //10, 13, 11,
//            //10, 12, 13,
//            //12, 15, 13,
//            //12, 14, 15,
//            //14, 9, 15,
//            //14, 8, 9,

//            //0, 10, 2, //inner
//            //10, 0, 8,
//            //2, 12, 4,
//            //12, 2, 10,
//            //4, 14, 6,
//            //14, 4, 12,
//            //6, 8, 0,
//            //8, 6, 14,

//        };
    #endregion
}

