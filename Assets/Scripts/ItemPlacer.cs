using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlacer : MonoBehaviour
{

    private Grid grid;

    //Find a reference to a Grid
    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
    }

    // Update is called once per frame
   private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
            {
                PlaceItemNear(hitInfo.point);
            }
        }
    }

    //Helper function to adjust selected point to the nearest point on the grid
    private void PlaceItemNear(Vector3 selectedPoint)
    {
        var finalPosition = grid.GetNearestPointOnGrid(selectedPoint);
        
    }
}
