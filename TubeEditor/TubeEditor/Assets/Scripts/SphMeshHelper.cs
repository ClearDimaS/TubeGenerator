using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphMeshHelper
{
    public bool includeOrNot(float start_phi, float stop_phi, float x, float y, float z)
    {
        float k1 = Mathf.Tan(start_phi * Mathf.Deg2Rad);
        float k2 = Mathf.Tan(stop_phi * Mathf.Deg2Rad);
        if (start_phi < 90 && stop_phi < 90)
        {
            return k1 * x < y && k2 * x > y;
        }
        else if (start_phi < 180 && stop_phi < 90)
        {
            return k1 * x > y && k2 * x > y;
        }
        else if (start_phi < 90 && stop_phi < 180)
        {
            return k1 * x < y && k2 * x < y;
        }
        if (start_phi < 180 && stop_phi < 180)
        {
            return k1 * x > y && k2 * x < y;
        }
        else if (start_phi < 270 && stop_phi < 90)
        {
            return k1 * x > y && k2 * x > y;
        }
        else
        if (start_phi < 270 && stop_phi < 180)
        {
            return k1 * x > y && k2 * x < y;
        }
        else if (start_phi < 90 && stop_phi < 270)
        {
            return k1 * x < y && k2 * x < y;
        }
        else if (start_phi < 180 && stop_phi < 270)
        {
            return k1 * x > y && k2 * x < y;
        }
        else if (start_phi < 270 && stop_phi < 270)
        {
            return k1 * x > y && k2 * x < y;
        }
        else
        if (start_phi < 360 && stop_phi < 90)
        {
            return k1 * x < y && k2 * x > y;
        }
        else if (start_phi < 360 && stop_phi < 180)
        {
            return k1 * x < y && k2 * x < y;
        }
        else if (start_phi < 360 && stop_phi < 270)
        {
            return k1 * x < y && k2 * x < y;
        }
        else
        if (start_phi < 90 && stop_phi < 360)
        {
            return k1 * x < y && k2 * x > y;
        }
        else if (start_phi < 180 && stop_phi < 360)
        {
            return k1 * x > y && k2 * x > y;
        }
        else if (start_phi < 270 && stop_phi < 360)
        {
            return k1 * x > y && k2 * x > y;
        }
        else
        {
            return k1 * x < y && k2 * x > y;
        }
    }
}
