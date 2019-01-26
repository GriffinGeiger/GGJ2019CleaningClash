using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    Stack<Item> Stash;
    public bool editable = true;
    private Vector3 screenPoint;
    private Vector3 offset;
    private ItemPlacer place = new ItemPlacer();

    void OnMouseDown()
    {
        if (editable)
        {
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }
    }

    void OnMouseDrag()
    {
        if (editable)
        {
            Vector3 cursorScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorScreenPoint) + offset;
            transform.position = place.PlaceItemNear(cursorPosition);
        }
    }

    int StorageLimit = 3;

    bool Store (Item i)
    {
        if (Stash.Count < StorageLimit)
        {
            Stash.Push(i);
            return true;
        }
        return false;

    }

    public Item Retrieve()
    {
        if (Stash.Count == 0)
        {
            return null;
        }
        return Stash.Pop();
    }
}
