using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private float stepTime;

    private Vector2Int currentDirection;
    public readonly List<Vector2Int> Directions = new List<Vector2Int>
        {Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right};

    private void Start()
    {
        transform.position = grid.GridToWorld(grid.GetRandomPosition());
    }

    public IEnumerator TryStep()
    {
        var newPosition = GetNewPosition();
        Vector2Int newGridPos = grid.WorldToGrid(newPosition);
        
        if (!grid.IsIndexValid(new Vector2Int(newGridPos.x, newGridPos.y)))
        {
            Debug.Log("Death");
        }
        
        RotateSprite(gameObject.transform, currentDirection);
        transform.position = newPosition;
        
        yield return new WaitForSeconds(stepTime);
        StartCoroutine(TryStep());
        
        if (grid.CollideFood())
        {
            Debug.Log("Food");
        }
    }

    private Vector3 GetNewPosition()
    {
        Vector3 step = (Vector3Int) currentDirection;
        step.x *= grid.TileSize.x;
        step.y *= grid.TileSize.y;

        Vector3 newPosition = transform.position + step;
        return newPosition;
    }

    public void UpdateDirection(Vector2Int direction)
    {
        if (CanUpdate(direction))
        {
            currentDirection = direction;
        }
    }
    
    private bool CanUpdate(Vector2Int direction)
    {
        bool notZero = direction != Vector2Int.zero;
        bool notOpposite = -direction != currentDirection;
        bool update = notOpposite && notZero;
        return update;
    }
    
    public void RotateSprite(Transform obj, Vector2Int dir)
    {
        if (dir == Directions[0])
        {
            obj.transform.eulerAngles = new Vector3(0, 0, -90);
        }
        else if (dir == Directions[1])
        {
            obj.transform.eulerAngles = new Vector3(0, 0, 90);
        }
        else if (dir == Directions[2])
        {
            obj.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (dir == Directions[3])
        {
            obj.transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
}
