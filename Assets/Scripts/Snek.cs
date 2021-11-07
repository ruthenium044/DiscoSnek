using System;
using System.Collections.Generic;
using UnityEngine;

public class Snek : MonoBehaviour
{
    private Movement movement;

    private void Start()
    {
        movement = GetComponent<Movement>();
        movement.UpdateDirection(movement.Directions[0]);
        StartCoroutine(movement.TryStep());
    }

    private void Update()
    {
        Vector2Int inputDirection = GetInput(movement.Directions);
        movement.UpdateDirection(inputDirection);
    }

    private Vector2Int GetInput(List<Vector2Int> directions)
    {
        Vector2Int direction = Vector2Int.zero;
        if (Input.GetKeyDown(KeyCode.W))
        {
            direction = directions[0];
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            direction = directions[1];
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            direction = directions[2];
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            direction = directions[3];
        }
        return direction;
    }
}
