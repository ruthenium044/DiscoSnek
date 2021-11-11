using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public bool CollideAndRemove(Transform transform, List<GameObject> other, Grid grid)
    {
        foreach (var item in other)
        {
            if (grid.WorldToGrid(transform.position) == grid.WorldToGrid(item.transform.position))
            {
                Destroy(item.gameObject);
                other.Remove(item);
                return true;
            } 
        }
        return false;
    }
    
    public bool Collide(Transform transform, List<GameObject> other, Grid grid)
    {
        foreach (var item in other)
        {
            if (grid.WorldToGrid(transform.position) == grid.WorldToGrid(item.transform.position))
            {
                return true;
            } 
        }
        return false;
    }
}
