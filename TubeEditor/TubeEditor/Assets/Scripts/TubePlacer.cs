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
    public void Init(Transform _parent)
    {
        parent = _parent;
        reset();
    }

    private float beta_prev;
    private float inner_R;
    private float num_steps;
    private float outter_R;
    private Vector3 point_start;
    private Vector3 point_end;
    private float cos_alpha_prev;
    private float cos_alpha_next;
    private float beta;
    bool y1_lrgr_y0;

    GameObject SphrPrefab;
    GameObject TubePrefab;

    public void placeTubes(int ind, GameObject _TubePrefab, GameObject _SphrPrefab, Vector3 _point_start, Vector3 _point_end, float tg_beta, bool x1_lrgr_x0, bool _y1_lrgr_y0, float _inner_R, float _outter_R, float _num_steps, float _cos_alpha_prev)
    {
        y1_lrgr_y0 = _y1_lrgr_y0;
        inner_R = _inner_R;
        num_steps = _num_steps;
        outter_R = _outter_R;
        point_start = _point_start;
        point_end = _point_end;
        cos_alpha_prev = _cos_alpha_prev;
        TubePrefab = _TubePrefab;
        SphrPrefab = _SphrPrefab;

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

        RotateTubes(ind);

        RotateSpheres(ind);

        beta_prev = beta % 360.0f;
    }

    void RotateTubes(int ind) 
    {
        Vector3 pos = calc_pos(point_start, point_end, TubePrefab, beta);

        TubesPositions[ind] = new Vector3(pos.x, pos.y, pos.z);

        TubesParents[ind] = Instantiate(TubePrefab, pos, Quaternion.identity);

        TubesParents[ind].transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Rad2Deg * beta);

        float height = Vector3.Distance(point_end, point_start);

        TubesParents[ind].GetComponentInChildren<MeshEditor>().UpdMesh(outter_R, Mathf.RoundToInt(num_steps), inner_R, height);

        TubesParents[ind].transform.SetParent(parent, true);

        TubesParents[ind].transform.position = TubesPositions[ind];
    }

    void RotateSpheres(int ind) 
    {

        if (TubesParents[ind].GetComponentInChildren<SphMeshEditor>() == null && cos_alpha_prev != Constants.IMPSBL_COS)
        {
            GameObject sphr = Instantiate(SphrPrefab, point_end, Quaternion.identity) as GameObject;

            sphr.transform.SetParent(TubesParents[ind].transform, true);

            TubesParents[ind].GetComponentInChildren<SphMeshEditor>().UpdMesh(outter_R, num_steps, beta * Mathf.Rad2Deg, beta_prev * Mathf.Rad2Deg);

            TubesParents[ind].GetComponentInChildren<SphMeshEditor>().gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
        }
        else
        {
            if (cos_alpha_prev != Constants.IMPSBL_COS)
                TubesParents[ind].GetComponentInChildren<SphMeshEditor>().UpdMesh(outter_R, num_steps, beta * Mathf.Rad2Deg, beta_prev * Mathf.Rad2Deg);
        }
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
