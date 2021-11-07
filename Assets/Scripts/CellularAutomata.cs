using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellularAutomata : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private int fillPercentage;
    [SerializeField] private int lifecycleIterations;
    
    private int[,] gridCA;

    public int[,] GridCa => gridCA;

    public void InitializeGrid(Vector2Int gridSize)
    {
        gridCA = new int[gridSize.x, gridSize.y];
        FillGrid(gridSize);
        for (int i = 0; i < lifecycleIterations; i++)
        {
            OneCycle(gridSize);
        }
    }

    private void FillGrid(Vector2Int gridSize)
    {
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                gridCA[i, j] = Random.Range(0, 1);
            }
        }
    }

    private void OneCycle(Vector2Int gridSize)
    {
        for (int i = 1; i < gridSize.x - 1; i++)
        {
            for (int j = 1; j < gridSize.y - 1; j++)
            {
                int neighbourCount = GetNeighbourCount(new Vector2Int(i, j));
                if (neighbourCount < 3 || neighbourCount > 3)
                {
                    gridCA[i, j] = 1;
                }
                else
                {
                    gridCA[i, j] = 0;
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
        int neighbourCount = gridCA[tilePosition.x, tilePosition.y + 1] + 
                             gridCA[tilePosition.x, tilePosition.y - 1] +
                             gridCA[tilePosition.x + 1, tilePosition.y] + 
                             gridCA[tilePosition.x - 1, tilePosition.y];
        return neighbourCount;
    }
}
