﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{

    //Size of each square in the grid
    public float size = 1;

    //Size of the entire grid
    private int gridSize = 40;

    //Draws spheres at corners of grid spaces
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for (float x = 0; x < gridSize; x+= size)
        {
            for (float y = 0; y < gridSize; y += size)
            {
                var point = GetNearestPointOnGrid(new Vector3(x, y, 0f));
                Gizmos.DrawSphere(point, 0.1f);
            }
        }
    }

    //Converts a given point to the closest point on the grid
    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        position -= transform.position;

        int xCount = Mathf.RoundToInt(position.x / size);
        int yCount = Mathf.RoundToInt(position.y / size);
        int zCount = Mathf.RoundToInt(position.z / size);

        Vector3 result = new Vector3(
            (float)xCount * size,
            (float)yCount * size,
            (float)zCount * size);

        result += transform.position;

        return result;
    }
}
