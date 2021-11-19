using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> foodPrefabs;
    [SerializeField] private float foodSpawnTime;
    [HideInInspector] public List<GameObject> foods;

    public IEnumerator SpawnFood(Grid grid)
    {
        SpawnRandomFood(grid);
        yield return new WaitForSeconds(foodSpawnTime);
        StartCoroutine(SpawnFood(grid));
    }
    
    private void SpawnRandomFood(Grid grid)
    {
        int nextIndex = grid.GetRandomInt(0, foodPrefabs.Count);
        GameObject temp = Instantiate(foodPrefabs[nextIndex]);
        temp.transform.position = grid.GridToWorld(grid.GetRandomPosition());
        foods.Add(temp);
    }
}
