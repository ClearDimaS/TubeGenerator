using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlacementController : MonoBehaviour
{
    //[SerializeField]
    //private GameObject placableObjectPrefab;

    //[SerializeField]
    //private KeyCode newObjectHotkey = KeyCode.A;

    //private GameObject currentPlacableObject;
    //private float mouseWheelRotation;

    //void Update()
    //{
    //    HandleNewObjectHotkey();

    //    if (currentPlacableObject != null) 
    //    {
    //        MoveCurrentPlaceableObjectToMouse();
    //        RotateFromMouseWheel();
    //        ReleaseIfClicked();
    //    }
    //}

    //private void ReleaseIfClicked()
    //{
    //    if (Input.GetMouseButtonDown(0)) 
    //    {
    //        currentPlacableObject = null;
    //    }
    //}

    //private void RotateFromMouseWheel()
    //{
    //    mouseWheelRotation += Input.mouseScrollDelta.y;
    //    currentPlacableObject.transform.Rotate(Vector3.up, mouseWheelRotation * 10f);
    //}

    //private void MoveCurrentPlaceableObjectToMouse()
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    //    RaycastHit hitInfo;
    //    if (Physics.Raycast(ray, out hitInfo)) 
    //    {
    //        currentPlacableObject.transform.position = hitInfo.point;
    //        currentPlacableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
    //    }
    //}

    //private void HandleNewObjectHotkey()
    //{
    //    if (Input.GetKeyDown(newObjectHotkey)) 
    //    {
    //        if (currentPlacableObject == null)
    //        {
    //            currentPlacableObject = Instantiate(placableObjectPrefab);
    //        }
    //        else 
    //        {
    //            Destroy(currentPlacableObject);
    //        }
    //    }
    //}
}
