using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private Vector2Int gridSize;
    [SerializeField] private Vector2Int tileSize;
    
    private GameObject[,] tiles;
    private CellularAutomata cellularAutomata;
    
    public GameObject[,] Tiles => tiles;

    public Vector2Int TileSize => tileSize;

    private void Awake()
    {
        cellularAutomata = GetComponent<CellularAutomata>();
        cellularAutomata.InitializeGrid(gridSize);
        
        tiles = new GameObject[gridSize.x, gridSize.y];
        FillGrid();
    }

    private void FillGrid()
    {
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                if (cellularAutomata.GridCa[i, j] == 1)
                {
                    CreateTile(i, j);
                }
            }
        }
    }

    private void CreateTile(int x, int y)
    {
        GameObject temp = Instantiate(tilePrefab);
        tiles[x, y] = temp;
        tiles[x, y].transform.position = new Vector3(x * tileSize.x, y * tileSize.y, 0);
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
        return tiles[gridPos.x, gridPos.y].transform.position;
    }

    public bool IsInBounds(Vector2Int gridPosition)
    {
        bool boundsX = gridPosition.x >= 0 && gridPosition.x < gridSize.x ;
        bool boundsY = gridPosition.y >= 0 && gridPosition.y < gridSize.y;
        return boundsX && boundsY;
    }
}
