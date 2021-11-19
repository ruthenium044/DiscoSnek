using UnityEngine;

public class CellularAutomata : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private int fillPercentage;
    [SerializeField] private int lifecycleIterations;
    private string seed;
    private int[,] grid;

    public int[,] Grid => grid;

    public void InitializeGrid(Vector2Int gridSize)
    {
        grid = new int[gridSize.x, gridSize.y];
        FillGrid(gridSize);
        for (int i = 0; i < lifecycleIterations; i++)
        {
            OneCycle(gridSize);
        }
    }

    private void FillGrid(Vector2Int gridSize)
    {
        seed =  System.DateTime.Now.ToString();
        System.Random pseudoRandom = new System.Random(seed.GetHashCode());
        
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                if (x == 0 || x == gridSize.x - 1 || y == 0 || y == gridSize.y - 1)
                {
                    grid[x, y] = 1;
                }
                else
                {
                    grid[x, y] = (pseudoRandom.Next(0, 100) < fillPercentage) ? 1 : 0;
                }
            }
        }
    }

    private void OneCycle(Vector2Int gridSize)
    {
        for (int x = 1; x < gridSize.x - 1; x++)
        {
            for (int y = 1; y < gridSize.y - 1; y++)
            {
                int neighbourCount = GetNeighbourCount(new Vector2Int(x, y));
                if (neighbourCount > 4)
                {
                    grid[x, y] = 1;
                }
                else if (neighbourCount < 4)
                {
                    grid[x, y] = 0;
                }
            }
        }
    }

    private int GetNeighbourCount(Vector2Int tilePosition)
    {
        int neighbourCount = 0;
        for (int neighbourX = tilePosition.x - 1; neighbourX <= tilePosition.x + 1; neighbourX++)
        {
            for (int neighbourY = tilePosition.y - 1; neighbourY <= tilePosition.y + 1; neighbourY++)
            {
                if (neighbourX != tilePosition.x || neighbourY != tilePosition.y)
                {
                    neighbourCount += grid[neighbourX, neighbourY];
                }
            }
        }
        return neighbourCount;
    }
}
