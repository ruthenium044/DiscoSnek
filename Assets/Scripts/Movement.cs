using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public readonly List<Vector2Int> Directions = new List<Vector2Int>
        {Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right};

    public bool TryStep(Grid grid, Body body, Vector2Int currentDirection)
    {
        var newPosition = GetNewPosition(grid, currentDirection);
        Vector2Int newGridPos = grid.WorldToGrid(newPosition);
        
        if (!grid.IsIndexValid(new Vector2Int(newGridPos.x, newGridPos.y)))
        {
            return false;
        }
        
        RotateSprite(currentDirection);
        
        Vector3 previousPosition = transform.position;
        Quaternion previousRotation = transform.rotation;
        transform.position = newPosition;
        
        body.MoveBodyParts(previousPosition, previousRotation);

        return true;
    }

    private Vector3 GetNewPosition(Grid grid, Vector2Int currentDirection)
    {
        Vector3 step = (Vector3Int) currentDirection;
        step.x *= grid.TileSize.x;
        step.y *= grid.TileSize.y;

        Vector3 newPosition = transform.position + step;
        return newPosition;
    }
    
    private void RotateSprite(Vector2Int dir)
    {
        if (dir == Directions[0])
        {
            transform.eulerAngles = new Vector3(0, 0, -90);
        }
        else if (dir == Directions[1])
        {
            transform.eulerAngles = new Vector3(0, 0, 90);
        }
        else if (dir == Directions[2])
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (dir == Directions[3])
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
}
