using System;
using UnityEngine;

public class Body : MonoBehaviour
{
    [SerializeField] private GameObject bodyPrefab;
    [SerializeField] private Sprite bodySprite;
    [SerializeField] private Sprite tailSprite;

    private LinkedList<GameObject> bodyParts;

    void Start()
    {
        bodyParts = new LinkedList<GameObject>();
        bodyParts.AddFront(gameObject);
        //spriteRenderer = GetComponent<SpriteRenderer>();
        //spriteRenderer.sprite = tailSprite;
    }

    public void AddBody()
    {
        GameObject temp = Instantiate(bodyPrefab);
        bodyParts.AddAfter(bodyParts.head, temp);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddBody();
        }
    }
}
