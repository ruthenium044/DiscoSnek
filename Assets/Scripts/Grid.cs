using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private Vector2Int gridSize;
    [SerializeField] private Vector2Int tileSize;
    
    private GameObject[,] grid;

    public Vector2Int TileSize => tileSize;

    private void FillGrid()
    {
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                GameObject temp = Instantiate(tilePrefab);
                grid[i, j] = temp;
            }
        }
    }

    public Vector2Int WorldToGrid(Vector3 worldPos)
    {
        Vector3 temp;
        //worldPos -= new Vector3(offset.x, offset.y, 0);
        temp.x = worldPos.x / tileSize.x;
        temp.y = worldPos.y / tileSize.y;
        return new Vector2Int((int)temp.x, (int)temp.y);
    }

    public Vector3 GridToWorld(Vector2Int gridPos)
    {
        return grid[gridPos.x, gridPos.y].transform.position;
    }
}
