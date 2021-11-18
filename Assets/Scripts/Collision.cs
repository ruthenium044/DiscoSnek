using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public bool CollideAndRemoveFood(Transform snakeTransform, List<GameObject> foods, Grid grid, out IPowerUp powerUp)
    {
        foreach (var item in foods)
        {
            if (grid.WorldToGrid(snakeTransform.position) == grid.WorldToGrid(item.transform.position))
            {
                GetComponent<Audio>().Play(Random.Range(1, 4));
                foods.Remove(item);
                powerUp = item.GetComponent<IPowerUp>();
                Destroy(item.gameObject);
                return true;
            } 
        }
        powerUp = null;
        return false;
    }
    
    public bool CollideBody(Transform snakeTransform, Body body, Grid grid)
    {
        var currentNode = body.BodyParts.head.nextNode;
        while (currentNode != null)
        {
            if (grid.WorldToGrid(snakeTransform.position) == grid.WorldToGrid(currentNode.Data.transform.position))
            {
                GetComponent<Audio>().Play(0);
                return true;
            }
            currentNode = currentNode.nextNode;
        }
        return false;
    }
}
