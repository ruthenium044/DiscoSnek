using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Snek : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private float stepTime;
    [SerializeField] private GameObject gameOverObject;
    [SerializeField] private float dropTime;
    [SerializeField] private Audio audio;
    
    private Movement movement;
    private InputHandling inputHandling;
    [HideInInspector] public Body body;
    
    private IPowerUp powerUp;
    [HideInInspector] public float speedModifier;
    [SerializeField] public GameObject screenOverlay;
 
    private void Awake()
    {
        screenOverlay.SetActive(false);
        gameOverObject.SetActive(false);
        movement = GetComponent<Movement>();
        inputHandling = GetComponent<InputHandling>();
        body = GetComponent<Body>();
    }

    private void Start()
    {
        transform.position = grid.GridToWorld(grid.GetStartPosition());
        StartCoroutine(Tick());
    }

    private IEnumerator Tick()
    {
        Vector2Int currentDirection = inputHandling.CurrentDirection;
        if (!movement.TryStep(grid, body, currentDirection) || grid.CollideBody())
        {
            StopAllCoroutines();
            StartCoroutine(ExecuteSnek());
        }
        inputHandling.IgnoreDirection(-currentDirection);
        yield return new WaitForSeconds(stepTime + speedModifier);
        
        StartCoroutine(Tick());
        EatFood();
    }

    private void EatFood()
    {
        if (grid.CollideFood(out var power))
        {
            powerUp = power;
            if (powerUp != null)
            {
                StartCoroutine(powerUp.StartPowerUp(this));
            }

            body.AddBodyPart();
        }
    }

    private IEnumerator ExecuteSnek() 
    {
        inputHandling.gameOver = true;
        audio.Play(0);
        StartCoroutine(DropGameOver());
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(0);
    }

    private IEnumerator DropGameOver()
    {
        gameOverObject.SetActive(true);
        float t = 0f;
        while (t < 1f)
        {
            gameOverObject.transform.localPosition = Vector3.Lerp(gameOverObject.transform.localPosition, 
                Vector3.zero, Mathf.Sqrt(1 - (t - 1) * (t - 1)));
            t += Time.deltaTime * dropTime;
            yield return null;
        }
    }
}
