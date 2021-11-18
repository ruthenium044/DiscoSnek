using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandling : MonoBehaviour
{
    private Vector2Int inputDirection;
    private Vector2Int currentDirection;
    private Movement movement;
    private Vector2Int ignoreDirection;
    [HideInInspector] public bool gameOver;

    public Vector2Int CurrentDirection => currentDirection;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        currentDirection = movement.Directions[0];
    }
    
    private void Update()
    {
        if (gameOver) return;
        inputDirection = GetInput(movement.Directions);
        UpdateDirection(inputDirection);
    }

    private void UpdateDirection(Vector2Int direction)
    {
        if (IsDirectionValid(direction))
        {
            currentDirection = direction;
        }
    }

    public void IgnoreDirection(Vector2Int direction)
    {
        ignoreDirection = direction;
    }

    private bool IsDirectionValid(Vector2Int direction)
    {
        bool notZero = direction != Vector2Int.zero;
        bool notIgnored = ignoreDirection != direction;
        bool isDirectionValid = notIgnored && notZero;
        return isDirectionValid;
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
