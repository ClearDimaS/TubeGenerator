using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UIElements;
//using UnityEngine.ProBuilder.MeshOperations;
//using UnityEngine.ProBuilder;
//using UnityEditor.ProBuilder;

[CustomEditor(typeof(PathCreator))]
public class PathEditor : Editor
{

    PathCreator creator;
    Path path;

    void OnSceneGUI()
    {
        Input();
        Draw();
    }

    bool gridApplied;
    void Input()
    {
        Event guiEvent = Event.current;
        Vector3 mousePos = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition).origin; // .GetPoint(Camera.main.transform.position.y)

        if (guiEvent.type == EventType.MouseDown && guiEvent.button == 0 && guiEvent.shift)
        {
            Undo.RecordObject(creator, "Add segment");
            path.AddSegment(mousePos);
            gridApplied = false;
            point_moved = true;
        }
        else
        {
            if ((size != settings.gridSize) && settings.enableGrid)
                applyGrid();
        }


        
        int closestAnchorIndex = -1;
        if (guiEvent.type == EventType.MouseDown && guiEvent.button == 1 && guiEvent.shift)
        {
            float minDstToAnchor = 1000.0f;//creator.anchorDiameter * .5f;
            

            for (int i = 0; i < path.NumPoints; i ++)
            {
                float dst = Vector3.Distance(new Vector3(mousePos.x, mousePos.y, 0.0f), path[i]);
                if (dst < minDstToAnchor)
                {
                    minDstToAnchor = dst;
                    closestAnchorIndex = i;
                }
            }
            point_moved = true;
        }

        if (closestAnchorIndex != -1)
        {
            tubePlacer.reset();
            Undo.RecordObject(creator, "Delete segment");
            path.DeleteSegment(closestAnchorIndex);
            tubePlacer.reset();
            for (int i = 0; i < path.NumSegments; i++)
            {
                Color tmp_unnescesary = getColor(i, path.GetPointsInSegment(i)[0], path.GetPointsInSegment(i)[1]);
            }
        }
    }

    float size;
    private void applyGrid()
    {
        size = settings.gridSize;
        
        for (int i = 0; i < path.NumPoints; i++)
            path.MovePoint(i, new Vector3(Mathf.RoundToInt(path[i].x / size) * size, Mathf.RoundToInt(path[i].y / size) * size, Mathf.RoundToInt(path[i].z / size) * size));

        gridApplied = true;
    }

    bool point_moved = false;
    void Draw()
    {
        for (int i = 0; i < path.NumSegments; i++)
        {
            Vector3[] points = path.GetPointsInSegment(i);
            Handles.color = getColor(i, points[1], points[0]);
            Handles.DrawLine(points[1], points[0]);
        }
        point_moved = false;

        Handles.color = Color.red;
        for (int i = 0; i < path.NumPoints; i++)
        {
            Vector3 newPos = Handles.FreeMoveHandle(path[i], Quaternion.identity, settings.outter_R / 2.0f, Vector3.zero, Handles.CylinderHandleCap);
            if (path[i] != new Vector3(newPos.x, newPos.y, 0.0f))
            {
                Undo.RecordObject(creator, "Move point");
                path.MovePoint(i, newPos);
                gridApplied = false;
                point_moved = true;
            }
            else 
            {
                if (!gridApplied && settings.enableGrid)
                    applyGrid();
            }
        }
    }

    private Color getColor(int i, Vector3 point_start, Vector3 point_end)
    {
        Color color = Color.black;
        float tg_beta = -((path[i].y - path[i + 1].y) / (path[i + 1].x - path[i].x));
        if (i < path.NumPoints - 1 && i > 0) 
        {
            float prod = ((path[i - 1].x - path[i].x) * (path[i].x - path[i + 1].x) + (path[i - 1].y - path[i].y) * (path[i].y - path[i + 1].y));

            float div = (Mathf.Abs(Vector3.Distance(path[i], path[i - 1])) * Mathf.Abs(Vector3.Distance(path[i], path[i + 1])));

            float cos_alpha = prod / div;

            if (cos_alpha > 0.9998f)
                color = Color.black;
            else if (cos_alpha > 0.01f)
                color = Color.black;
            else if (cos_alpha > -0.01f)
                color = Color.red;
            else
                color = Color.red;
            if(point_moved)
                tubePlacer.placeTubes(i, creator.TubePrefab, creator.SphrPrefab, point_start, point_end, tg_beta, path[i + 1].x > path[i].x, path[i + 1].y > path[i].y, settings.inner_R, settings.outter_R, settings.num_steps, cos_alpha);
        }

        if (i == 0) 
        {
            if (point_moved)
                tubePlacer.placeTubes(i, creator.TubePrefab, creator.SphrPrefab, point_start, point_end, tg_beta, path[i + 1].x > path[i].x, path[i + 1].y > path[i].y, settings.inner_R, settings.outter_R, settings.num_steps, Constants.IMPSBL_COS);
        }

        return color;
    }

    Settings settings;

    static TubePlacer tubePlacer;

    void OnEnable()
    {
        creator = (PathCreator)target;
        if (creator.path == null)
        {
            creator.CreatePath();
        }
        path = creator.path;
        settings = GameObject.FindObjectOfType<Settings>();
        tubePlacer = new TubePlacer(creator.Parent);
        point_moved = true;
    }
}