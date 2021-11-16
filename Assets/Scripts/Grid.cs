using System;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private Vector2Int gridSize;
    [SerializeField] private Vector2Int tileSize;
    
    private GameObject[,] tiles;
    private List<Vector2Int> playableTiles;
    private CellularAutomata cellularAutomata;
    private FoodSpawner foodSpawner;
    private Body body;

    [SerializeField] private Snek snek;
    private Collision collision;
    
    private string seed;
    private System.Random pseudoRandom;
    
    public Vector2Int TileSize => tileSize;

    private void Awake()
    {
        seed = DateTime.Now.ToString();
        pseudoRandom = new System.Random(seed.GetHashCode());
        
        cellularAutomata = GetComponent<CellularAutomata>();
        cellularAutomata.InitializeGrid(gridSize);
        
        foodSpawner = GetComponent<FoodSpawner>();
        body = snek.GetComponent<Body>();
        
        collision = GetComponent<Collision>();
        
        playableTiles = GetComponent<FloodFill>().GetBiggestCave(cellularAutomata.Grid);
        tiles = new GameObject[gridSize.x, gridSize.y];
        FillGrid();
    }

    private void Start()
    {
        StartCoroutine(foodSpawner.SpawnFood(this));
    }

    private void FillGrid()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                if (playableTiles.Contains(new Vector2Int(x, y)))
                {
                    CreateTile(x, y);
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

    public bool CollideFood(out IPowerUp powerUp)
    {
        return collision.CollideAndRemoveFood(snek.transform, foodSpawner.foods, this, out powerUp);
    }

    public bool CollideBody()
    {
        return collision.CollideBody(snek.transform, body, this);
    }

    public Vector2Int WorldToGrid(Vector3 worldPos)
    {
        Vector3 temp;
        temp.x = worldPos.x / tileSize.x;
        temp.y = worldPos.y / tileSize.y;
        return new Vector2Int((int)temp.x, (int)temp.y);
    }

    public Vector3 GridToWorld(Vector2Int gridPos)
    {
        return tiles[gridPos.x, gridPos.y].transform.position;
    }

    public bool IsIndexValid(Vector2Int gridPosition)
    {
        return playableTiles.Contains(gridPosition);
    }

    public Vector2Int GetRandomPosition()
    {
        return playableTiles[pseudoRandom.Next(0, playableTiles.Count)];
    }

    public int GetRandomInt(int min, int max)
    {
        return pseudoRandom.Next(min, max);
    }
    
}
