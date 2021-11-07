using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellularAutomata : MonoBehaviour
{
    private int[,] grid = new int[,] { };

    public void InitializeGrid(Vector2 gridSize, int cycleCount)
    {
        FillGrid(gridSize);
        for (int i = 0; i < cycleCount; i++)
        {
            OneCycle(gridSize);
        }
    }

    private void FillGrid(Vector2 gridSize)
    {
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                grid[i, j] = Random.Range(0, 1);
            }
        }
    }

    private void OneCycle(Vector2 gridSize)
    {
        for (int i = 1; i < gridSize.x - 1; i++)
        {
            for (int j = 1; j < gridSize.y - 1; j++)
            {
                int neighbourCount = GetNeighbourCount(new Vector2Int(i, j));
                if (neighbourCount < 3 || neighbourCount > 3)
                {
                    grid[i, j] = 1;
                }
                else
                {
                    grid[i, j] = 0;
                }
            }
        }
    }

    private void RemoveInacesable()
    {
        //fill each 0. find biggest one. remove the rest
    }
    
    private int GetNeighbourCount(Vector2Int tilePosition)
    {
        int neighbourCount = grid[tilePosition.x, tilePosition.y + 1] + 
                             grid[tilePosition.x, tilePosition.y - 1] +
                             grid[tilePosition.x + 1, tilePosition.y] + 
                             grid[tilePosition.x - 1, tilePosition.y];
        return neighbourCount;
    }
}
