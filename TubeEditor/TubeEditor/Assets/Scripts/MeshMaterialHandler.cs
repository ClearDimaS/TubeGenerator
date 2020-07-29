using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeshMaterialHandler
{
    private Vector2 ConvertPixelsToUVCoordinates(float x, float y, float textureWidth, float textureHeight)
    {
        x = ((float)x / textureWidth);
        y = ((float)y) / (textureHeight + 0.1f);

        return new Vector2(x, y);
    }

    public Vector2[] GetUVRectangleFromPixels(int numSteps, int textureWidth, int textureHeight) // List<int> top, List<int> bot, List<int> outter, List<int> inner, 
    {
        /*every Tube is generated in 4 steps: bottom ring(circle) , top ring(circle), outter side and inner side
        * each of the listed 4 consists of numSteps rectangles and every rectangle is divided into 2 triangles. But here we just need to divide image into 4 * numSegments rectangles;
        */

        int numLines = 4; // i have decided to split texture image into 4 horizontal segments so each of them represents its own side of a Tube

        int height = textureHeight / numLines;
        int width = textureWidth / (numSteps + 2);

        int num;
        List<Vector2> res = new List<Vector2>();

        num = (int)((numSteps + 1) / 2.0f);

        int y = 0;
        for (int x = 0; x < num; x++)
        {
            res.Add(ConvertPixelsToUVCoordinates((x + 1) * width, (y) * height, textureWidth, textureHeight));
            res.Add(ConvertPixelsToUVCoordinates((x) * width, (y + 1) * height, textureWidth, textureHeight));
            res.Add(ConvertPixelsToUVCoordinates((x) * width, (y) * height, textureWidth, textureHeight));
            res.Add(ConvertPixelsToUVCoordinates((x + 1) * width, (y + 1) * height, textureWidth, textureHeight));
        }
        y = 1;
        for (int x = 0; x < num; x++)
        {
            res.Add(ConvertPixelsToUVCoordinates((x + 1) * width, (y) * height, textureWidth, textureHeight));
            res.Add(ConvertPixelsToUVCoordinates((x) * width, (y + 1) * height, textureWidth, textureHeight));
            res.Add(ConvertPixelsToUVCoordinates((x) * width, (y) * height, textureWidth, textureHeight));
            res.Add(ConvertPixelsToUVCoordinates((x + 1) * width, (y + 1) * height, textureWidth, textureHeight));
        }
        y = 2;
        for (int x = 0; x < num; x++)
        {
            res.Add(ConvertPixelsToUVCoordinates((x + 1) * width, (y) * height, textureWidth, textureHeight));
            res.Add(ConvertPixelsToUVCoordinates((x) * width, (y + 1) * height, textureWidth, textureHeight));
            res.Add(ConvertPixelsToUVCoordinates((x) * width, (y) * height, textureWidth, textureHeight));
            res.Add(ConvertPixelsToUVCoordinates((x + 1) * width, (y + 1) * height, textureWidth, textureHeight));
        }
        y = 3;
        for (int x = 0; x < num; x++)
        {
            res.Add(ConvertPixelsToUVCoordinates((x + 1) * width, (y) * height, textureWidth, textureHeight));
            res.Add(ConvertPixelsToUVCoordinates((x) * width, (y + 1) * height, textureWidth, textureHeight));
            res.Add(ConvertPixelsToUVCoordinates((x) * width, (y) * height, textureWidth, textureHeight));
            res.Add(ConvertPixelsToUVCoordinates((x + 1) * width, (y + 1) * height, textureWidth, textureHeight));
        }

        return res.ToArray();
    }

    public Vector3[] GetNormals(Vector3[] verticles, int numSteps)
    {
        int num;
        List<Vector3> res = new List<Vector3>();

        num = (int)((numSteps + 1) / 2.0f);
        for (int i = 0; i < verticles.Length; i += 4)
        {
            if (i < verticles.Length / 4)
            {
                res.Add(Normal(verticles[i + 2], verticles[i + 1], verticles[i]));
                res.Add(Normal(verticles[i + 2], verticles[i + 3], verticles[i + 1]));
                res.Add(Normal(verticles[i + 3], verticles[i], verticles[i + 2]));
                res.Add(Normal(verticles[i + 1], verticles[i], verticles[i + 3]));
            }
            else if (i < 2 * verticles.Length / 4) 
            {
                res.Add(Normal(verticles[i], verticles[i + 1], verticles[i + 2]));
                res.Add(Normal(verticles[i + 1], verticles[i + 3], verticles[i + 2]));
                res.Add(Normal(verticles[i + 2], verticles[i], verticles[i + 3]));
                res.Add(Normal(verticles[i + 3], verticles[i], verticles[i + 1]));
            }
            else if (i < 3 * verticles.Length / 4)
            {
                res.Add(Normal(verticles[i], verticles[i + 2], verticles[i + 1]));
                res.Add(Normal(verticles[i + 1], verticles[i + 2], verticles[i + 3]));
                res.Add(Normal(verticles[i + 2], verticles[i + 3], verticles[i]));
                res.Add(Normal(verticles[i + 3], verticles[i + 1], verticles[i]));
            }
            else
            {
                res.Add(Normal(verticles[i], verticles[i + 1], verticles[i + 2]));
                res.Add(Normal(verticles[i + 1], verticles[i + 3], verticles[i + 2]));
                res.Add(Normal(verticles[i + 2], verticles[i], verticles[i + 3]));
                res.Add(Normal(verticles[i + 3], verticles[i], verticles[i + 1]));
            }

        }

        return res.ToArray();
    }

    public Vector3 Normal(Vector3 b, Vector3 a, Vector3 c)
    {
        var dir = Vector3.Cross(b - a, c - a);
        var norm = Vector3.Normalize(dir);
        return norm;
    }
}
