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
    public Vector3 PlaceItemNear(Vector3 selectedPoint)
    {
        return grid.GetNearestPointOnGrid(selectedPoint);
        
        //Video was just creating a cube here so we need to figure something else out

    }
}
