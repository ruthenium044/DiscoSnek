using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Snek : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private float stepTime;
    private Movement movement;
    private Body body;
    private Vector2Int currentDirection;
    
    bool gameOver;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        body = GetComponent<Body>();
    }

    private void Start()
    {
        UpdateDirection(movement.Directions[0]);
        transform.position = grid.GridToWorld(grid.GetRandomPosition());
        StartCoroutine(Tick());
    }

    private IEnumerator Tick()
    {
        if (!movement.TryStep(grid, body, currentDirection) || grid.CollideBody())
        {
            StopAllCoroutines();
            StartCoroutine(ExecuteSnek());
        }

        yield return new WaitForSeconds(stepTime);
        StartCoroutine(Tick());
        if (grid.CollideFood())
        {
            body.AddBodyPart();
        }
    }

    private IEnumerator ExecuteSnek()
    {
        gameOver = true;
        Debug.Log("Game over");
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(0);
    }

    //Handle input?
    private void Update()
    {
        if (gameOver) return;
        Vector2Int inputDirection = GetInput(movement.Directions);
        UpdateDirection(inputDirection);
    }
    
    private void UpdateDirection(Vector2Int direction)
    {
        if (IsDirectionValid(direction))
        {
            currentDirection = direction;
        }
    }
    
    private bool IsDirectionValid(Vector2Int direction)
    {
        bool notZero = direction != Vector2Int.zero;
        bool notOpposite = -direction != currentDirection;
        bool isDirectionValid = notOpposite && notZero;
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
