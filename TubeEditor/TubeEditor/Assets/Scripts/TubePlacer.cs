using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UIElements;
using UnityEngine.ProBuilder;

public class TubePlacer : Editor
{
    static List<GameObject> TubesParents;

    static List<Vector3> TubesPositions;

    Transform parent;
    public TubePlacer(Transform _parent)
    {
        parent = _parent;
        reset();
    }

    float beta_prev;

    public void placeTubes(int ind, GameObject TubePrefab, GameObject SphrPrefab, Vector3 point_start, Vector3 point_end, float tg_beta, bool x1_lrgr_x0, bool y1_lrgr_y0, float inner_R, float outter_R, float num_steps, float cos_alpha)
    {
        float beta;
        if (x1_lrgr_x0)
            beta = Mathf.Atan(tg_beta);
        else
            beta = Mathf.Atan(tg_beta) + Mathf.PI;

        if (beta < 0.0f)
            beta += 2 * Mathf.PI;

        if (ind > TubesParents.Count - 1 || ind > TubesPositions.Count - 1)
            _add();



        if (TubesParents[ind] != null)
            DestroyImmediate(TubesParents[ind]);
        if (TubesPositions[ind] != calc_pos(point_start, point_end, TubePrefab, beta)) 
        {

        }
        Vector3 pos = calc_pos(point_start, point_end, TubePrefab, beta);

        TubesPositions[ind] = new Vector3(pos.x, pos.y, pos.z);

        TubesParents[ind] = Instantiate(TubePrefab, pos, Quaternion.identity);

        TubesParents[ind].transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Rad2Deg * beta);

        float height = Vector3.Distance(point_end, point_start);

        TubesParents[ind].GetComponentInChildren<MeshEditor>().UpdMesh(outter_R, Mathf.RoundToInt(num_steps), inner_R, height);

        TubesParents[ind].transform.SetParent(parent, true);

        TubesParents[ind].transform.position = TubesPositions[ind];

        if (TubesParents[ind].GetComponentInChildren<SphMeshEditor>() == null && cos_alpha != Constants.IMPSBL_COS)
        {
            GameObject sphr = Instantiate(SphrPrefab, point_end, Quaternion.identity) as GameObject;

            sphr.transform.SetParent(TubesParents[ind].transform, true);

            TubesParents[ind].GetComponentInChildren<SphMeshEditor>().UpdMesh(outter_R, num_steps, beta * Mathf.Rad2Deg, beta_prev * Mathf.Rad2Deg);

            TubesParents[ind].GetComponentInChildren<SphMeshEditor>().gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
        }
        else
        {
            if (cos_alpha != Constants.IMPSBL_COS)
                TubesParents[ind].GetComponentInChildren<SphMeshEditor>().UpdMesh(outter_R, num_steps, beta * Mathf.Rad2Deg, beta_prev * Mathf.Rad2Deg);
        }

        beta_prev = beta % 360.0f;
    }

    private Vector3 calc_pos(Vector3 point_start, Vector3 point_end, GameObject prefab, float beta)
    {
        return (point_start + point_end) / 2.0f;
    }

    public void reset() 
    {
        if (TubesParents != null) 
        {
            for (int i = 0; i < TubesParents.Count; i++)
            {
                _remove(i);
            }
        }


        for (int i = 0; i < parent.childCount; i++)
        {
            DestroyImmediate(parent.GetChild(i).gameObject);
        }

        TubesParents = new List<GameObject>();
        TubesPositions = new List<Vector3>();
    }
    
    void _remove(int index) 
    {
        DestroyImmediate(TubesParents[index]);
        TubesPositions[index] = new Vector3(0.0f, 0.0f, 0.0f);
    }

    void _add() 
    {
        TubesParents.Add(null);
        TubesPositions.Add(new Vector3(0.0f, 0.0f, 0.0f));
    }
}
