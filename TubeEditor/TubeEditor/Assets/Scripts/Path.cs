using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[System.Serializable]
public class Path
{

    [SerializeField, HideInInspector]
    List<Vector3> points;

    public Path(Vector3 centre)
    {
        points = new List<Vector3>
        {
            centre+Vector3.left,
            centre + Vector3.right
        };
    }

    public Vector3 this[int i]
    {
        get
        {
            return points[i];
        }
    }

    public int NumPoints
    {
        get
        {
            return points.Count;
        }
    }

    public int NumSegments
    {
        get
        {
            return (points.Count - 1);
        }
    }

    public void AddSegment(Vector3 anchorPos)
    {
        points.Add(new Vector3(anchorPos.x, anchorPos.y, 0.0f));
    }

    public Vector3[] GetPointsInSegment(int i)
    {
        return new Vector3[] { points[i], points[i+ 1] };
    }

    Vector3 newPos;
    public void MovePoint(int i, Vector3 pos)
    {
        newPos = new Vector3(pos.x, pos.y, 0.0f);
        points[i] = newPos;
    }

    public void DeleteSegment(int anchorIndex)
    {
        points.Remove(points[anchorIndex]);
    }

}