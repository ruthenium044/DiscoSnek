using System;
using UnityEngine;

public class Body : MonoBehaviour
{
    [SerializeField] private GameObject bodyPrefab;
    [SerializeField] private GameObject tailPrefab;

    private LinkedList<GameObject> bodyParts;

    void Awake()
    {
        AddHead();
        AddBody(tailPrefab);
    }

    private void AddHead()
    {
        bodyParts = new LinkedList<GameObject>();
        bodyParts.AddFront(gameObject);
    }

    public void AddBody(GameObject prefab)
    {
        GameObject temp = Instantiate(prefab);
        bodyParts.AddAfter(bodyParts.head, temp);
    }

    public void MoveBodyParts(Vector3 previousPosition)
    {
        var currentNode = bodyParts.head.nextNode;
        while (currentNode != null)
        {
            (currentNode.Data.transform.position, previousPosition) = 
                (previousPosition, currentNode.Data.transform.position);
            currentNode = currentNode.nextNode;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddBody(bodyPrefab);
        }
    }
}
