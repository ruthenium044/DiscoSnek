using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodFill : MonoBehaviour
{
    public void OptimizeGrid(int[,] grid)
    {
        Vector2Int gridSize = new Vector2Int(grid.GetLength(0), grid.GetLength(1));
        List<Vector2Int> allTiles = GetAllTiles(grid, gridSize);
        bool[,] visited = new bool[grid.GetLength(0), grid.GetLength(1)];
        
        List<List<Vector2Int>> caves = new List<List<Vector2Int>>();
        int biggestCaveIndex = 0;
        biggestCaveIndex = GetBiggestCave(grid, allTiles, visited, caves, biggestCaveIndex);
        DeleteSmallCaves(grid, caves, biggestCaveIndex);
    }

    private void Flood(int[,] grid, Vector2Int currentPosition, bool[,] visited, List<Vector2Int> currentCave)
    {
        if (visited[currentPosition.x,currentPosition.y])
        {
           return;
        }

        visited[currentPosition.x, currentPosition.y] = true;
        currentCave.Add(new Vector2Int(currentPosition.x, currentPosition.y));
        for (int neighbourX = currentPosition.x - 1; neighbourX <= currentPosition.x + 1; neighbourX++)
        {
            for (int neighbourY = currentPosition.y - 1; neighbourY <= currentPosition.y + 1; neighbourY++)
            {
                if (neighbourX == currentPosition.x && neighbourY == currentPosition.y)
                {
                    continue;
                }
                if (IsPositionValid(grid, neighbourX, neighbourY))
                {
                    if (grid[neighbourX, neighbourY] == 0)
                    {
                        Flood(grid, new Vector2Int(neighbourX, neighbourY), visited, currentCave);
                    }
                }
            }
        }
    }
    
    private static List<Vector2Int> GetAllTiles(int[,] grid, Vector2Int gridSize)
    {
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

    private int GetBiggestCave(int[,] grid, List<Vector2Int> allTiles, bool[,] visited, List<List<Vector2Int>> caves, int biggestCaveIndex)
    {
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

    private static void DeleteSmallCaves(int[,] grid, List<List<Vector2Int>> caves, int biggestCaveIndex)
    {
        foreach (var cave in caves)
        {
            if (cave != caves[biggestCaveIndex])
            {
                foreach (var position in cave)
                {
                    grid[position.x, position.y] = 1;
                }
            }
        }
    }

    private bool IsPositionValid(int[,] grid, int x, int y)
    {
        Vector2Int gridSize = new Vector2Int(grid.GetLength(0), grid.GetLength(1));
        return x >= 0 && y >= 0 && x <= gridSize.x && y <= gridSize.y;
    }
}
