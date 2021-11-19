using System.Collections.Generic;
using UnityEngine;

public class FloodFill : MonoBehaviour
{
    public List<Vector2Int> GetBiggestCave(int[,] grid)
    {
        List<Vector2Int> allTiles = GetAllTiles(grid);
        List<List<Vector2Int>> caves = new List<List<Vector2Int>>();
        
        int biggestCaveIndex = 0;
        biggestCaveIndex = GetBiggestCave(grid, allTiles, caves, biggestCaveIndex);
        return caves[biggestCaveIndex];
    }

    private void Flood(int[,] grid, Vector2Int currentPosition, bool[,] visited, List<Vector2Int> currentCave)
    {
        if (visited[currentPosition.x,currentPosition.y])
        {
           return;
        }
        visited[currentPosition.x, currentPosition.y] = true;
        currentCave.Add(new Vector2Int(currentPosition.x, currentPosition.y));

        Vector2Int[] neighbours = GetNeighbours(currentPosition);
        for (int i = 0; i < neighbours.Length; i++)
        {
            if (neighbours[i].x == currentPosition.x && neighbours[i].y == currentPosition.y)
            {
                continue;
            }

            if (IsPositionValid(grid, neighbours[i].x, neighbours[i].y))
            {
                if (grid[neighbours[i].x, neighbours[i].y] == 0)
                {
                    Flood(grid, new Vector2Int(neighbours[i].x, neighbours[i].y), visited, currentCave);
                }
            }
        }
    }

    private Vector2Int[] GetNeighbours(Vector2Int currentPosition)
    {
        Vector2Int neighbour1 = new Vector2Int(currentPosition.x + 1, currentPosition.y);
        Vector2Int neighbour2 = new Vector2Int(currentPosition.x - 1, currentPosition.y);
        Vector2Int neighbour3 = new Vector2Int(currentPosition.x, currentPosition.y + 1);
        Vector2Int neighbour4 = new Vector2Int(currentPosition.x, currentPosition.y - 1);
        return new[] {neighbour1, neighbour2, neighbour3, neighbour4};
    }
    
    private static List<Vector2Int> GetAllTiles(int[,] grid)
    {
        Vector2Int gridSize = new Vector2Int(grid.GetLength(0), grid.GetLength(1));
        List<Vector2Int> tiles = new List<Vector2Int>();
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                if (grid[x, y] == 0)
                {
                    tiles.Add(new Vector2Int(x, y));
                }
            }
        }
        return tiles;
    }

    private int GetBiggestCave(int[,] grid, List<Vector2Int> allTiles, List<List<Vector2Int>> caves, int biggestCaveIndex)
    {
        bool[,] visited = new bool[grid.GetLength(0), grid.GetLength(1)];
        List<Vector2Int> biggestCave = new List<Vector2Int>();
        int count = 0;
        foreach (var index in allTiles)
        {
            if (!visited[index.x, index.y])
            {
                caves.Add(new List<Vector2Int>());
                Flood(grid, index, visited, caves[count]);
                count++;
            }

            int currentCaveIndex = count - 1;
            List<Vector2Int> currentCave = caves[currentCaveIndex];
            if (count > 1)
            {
                if (currentCave.Count > biggestCave.Count)
                {
                    biggestCaveIndex = currentCaveIndex;
                    biggestCave = currentCave;
                }
            }
            else
            {
                biggestCave = currentCave;
            }
        }
        return biggestCaveIndex;
    }
    
    private static bool IsPositionValid(int[,] grid, int x, int y)
    {
        Vector2Int gridSize = new Vector2Int(grid.GetLength(0), grid.GetLength(1));
        return x >= 0 && y >= 0 && x <= gridSize.x && y <= gridSize.y;
    }
}
