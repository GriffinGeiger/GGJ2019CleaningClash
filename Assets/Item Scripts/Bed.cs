using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : DraggableObject
{
    Stack<Item> Stash;

    int StorageLimit = 3;

    public bool Store (Item i)
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
