using System;
using UnityEngine;

public class Body : MonoBehaviour
{
    [SerializeField] private GameObject bodyPrefab;
    [SerializeField] private GameObject tailPrefab;

    private LinkedList<GameObject> bodyParts;

    public LinkedList<GameObject> BodyParts => bodyParts;

    void Awake()
    {
        AddHead();
    }

    private void Start()
    {
        AddBodyPart(tailPrefab);
    }

    private void AddHead()
    {
        bodyParts = new LinkedList<GameObject>();
        bodyParts.AddFront(gameObject);
    }
    
    public void AddBodyPart()
    {
        AddBodyPart(bodyPrefab);
    }

    private void AddBodyPart(GameObject prefab)
    {
        GameObject temp = Instantiate(prefab);
        temp.transform.position = bodyParts.head.Data.transform.position; 
        temp.transform.rotation = bodyParts.head.Data.transform.rotation; 
        bodyParts.AddAfter(bodyParts.head, temp);
    }

    public void MoveBodyParts(Vector3 previousPosition, Quaternion previousRotation)
    {
        var currentNode = bodyParts.head.nextNode;
        while (currentNode != null)
        {
            (currentNode.Data.transform.position, previousPosition) = 
                (previousPosition, currentNode.Data.transform.position);
            
            (currentNode.Data.transform.rotation, previousRotation) = 
                (previousRotation, currentNode.Data.transform.rotation);
           
            currentNode = currentNode.nextNode;
        }
    }
}
