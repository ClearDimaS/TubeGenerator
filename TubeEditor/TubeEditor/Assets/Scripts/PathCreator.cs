using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCreator : MonoBehaviour
{

    [HideInInspector]
    public Path path;

    public float anchorDiameter = .1f;

    public GameObject TubePrefab;

    public GameObject SphrPrefab;

    public Transform Parent;
    public void CreatePath()
    {
        path = new Path(new Vector3(0.0f, 0.0f, 0.0f));
    }

    private void Reset()
    {
        CreatePath();
    }
}