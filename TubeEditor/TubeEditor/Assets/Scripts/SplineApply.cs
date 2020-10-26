using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class SplineApply : MonoBehaviour
{
    [SerializeField]
    public GameObject pathEditObj; // who has a component PathEditor

    [Header("Press me to generate spline")]
    [SerializeField]
    public bool Generate;

    [Header("Spline parameters")]
    [Tooltip("Start with x in power of 0 and so on")]
    [SerializeField]
    List<float> SplineCoefficient = new List<float>();

    [SerializeField]
    float point_start = 0;

    [SerializeField]
    float point_end = 0;

    public GameObject obj;

    [SerializeField]
    float points_number = 0;

    [Header("Use X or Y axis")]
    [Tooltip("Swaps X and Y axis")]
    [SerializeField]
    bool XYInvert;

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Generating");
        if (Generate == true) 
        {
            Generate = false;
            generate();
        }
    }

    private void generate()
    {
        PathCreator pathEdit = pathEditObj.GetComponentInChildren<PathCreator>();
        Path path = pathEdit.path;
        float step = (point_end - point_start) / points_number;

        if (XYInvert)
        {
            for (float current_x = point_start; current_x < point_end; current_x += step)
                path.AddSegment(path[path.NumPoints - 1] + new Vector3(current_x, CalcSplineInPoint(current_x), 0));
        }
        else 
        {
            for (float current_x = point_start; current_x < point_end; current_x += step)
                path.AddSegment(path[path.NumPoints - 1] + new Vector3(CalcSplineInPoint(current_x), current_x, 0));
        }

        PathEditor.instance.SplineApplied();
    }

    float CalcSplineInPoint(float x) 
    {
        float res = 0;
        for (int i = 0; i < SplineCoefficient.Count; i++)
            res+= Mathf.Pow(x, i) * SplineCoefficient[i];

        return res;
    }
}
