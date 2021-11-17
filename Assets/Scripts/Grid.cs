using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private Vector2Int gridSize;
    [SerializeField] private Vector2Int tileSize;
    [SerializeField] private Camera camera;

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
        SetCameraSize();
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
        return new Vector2Int((int) temp.x, (int) temp.y);
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

    public Vector2Int GetStartPosition()
    {
        int smallestY = gridSize.y - 1;
        foreach (var tile in playableTiles)
        {
            if (tile.y < smallestY)
            {
                smallestY = tile.y;
            }
        }

        List<Vector2Int> smallestTiles = new List<Vector2Int>();
        foreach (var tile in playableTiles)
        {
            if (tile.y == smallestY)
            {
                smallestTiles.Add(tile);
            }
        }

        Vector2Int kindaMiddleTile = smallestTiles[smallestTiles.Count / 2];
        int index = playableTiles.IndexOf(kindaMiddleTile);
        return playableTiles[index];
    }

    private void SetCameraSize()
    {
        float screenRatio = Screen.width / Screen.height;

        Vector4 playableSize = GetPlayableSize();
        Vector2 bottomCorner = new Vector2(playableSize.x * tileSize.x, playableSize.y * tileSize.y);
        Vector2 topCorner = new Vector2(playableSize.z * tileSize.x, playableSize.w * tileSize.y);
        Vector3 mid = new Vector3((topCorner.x + bottomCorner.x) / 2, (topCorner.y + bottomCorner.y) / 2,
            camera.transform.position.z);
        
        //todo tweak this
        float gridRatio = topCorner.x / topCorner.y;

        if (screenRatio > gridRatio)
        {
            camera.orthographicSize = topCorner.y / 2;
        }
        else
        {
            float difference = gridRatio / screenRatio;
            camera.orthographicSize = topCorner.y / 2 * difference;
        }
        camera.transform.position = mid;
    }

    private Vector4 GetPlayableSize()
    {
        Vector2Int smallestPoints = gridSize;
        Vector2Int biggestPoints = Vector2Int.zero;
        foreach (var tile in playableTiles)
        {
            if (tile.y < smallestPoints.y)
            {
                smallestPoints.y = tile.y;
            }

            if (tile.x < smallestPoints.x)
            {
                smallestPoints.x = tile.x;
            }

            if (tile.y > biggestPoints.y)
            {
                biggestPoints.y = tile.y;
            }

            if (tile.x > biggestPoints.x)
            {
                biggestPoints.x = tile.x;
            }
        }
        return new Vector4(smallestPoints.x, smallestPoints.y, biggestPoints.x, biggestPoints.y);
    }

}
