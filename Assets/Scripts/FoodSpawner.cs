using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private GameObject foodPrefab;
    [SerializeField] private float foodSpawnTime;

    public List<GameObject> foods;
    
    public IEnumerator SpawnFood(Grid grid)
    {
        SpawnRandomFood(grid);
        yield return new WaitForSeconds(foodSpawnTime);
        StartCoroutine(SpawnFood(grid));
    }
    
    private void SpawnRandomFood(Grid grid)
    {
        GameObject temp = Instantiate(foodPrefab);
        temp.transform.position = grid.GridToWorld(grid.GetRandomPosition());
        foods.Add(temp);
    }
}
