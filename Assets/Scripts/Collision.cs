using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public bool CollideAndRemoveFood(Transform snakeTransform, List<GameObject> foods, Grid grid)
    {
        foreach (var item in foods)
        {
            if (grid.WorldToGrid(snakeTransform.position) == grid.WorldToGrid(item.transform.position))
            {
                foods.Remove(item);
                Destroy(item.gameObject);
                return true;
            } 
        }
        return false;
    }
    
    public bool CollideBody(Transform snakeTransform, Body body, Grid grid)
    {
        var currentNode = body.BodyParts.head.nextNode;
        while (currentNode != null)
        {
            if (grid.WorldToGrid(snakeTransform.position) == grid.WorldToGrid(currentNode.Data.transform.position))
            {
                return true;
            }
            currentNode = currentNode.nextNode;
        }
        return false;
    }
}
