using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedList<T>
{
    public class Node
    {
        private T data;
        internal Node nextNode;

        public T Data
        {
            get => data;
            set => data = value;
        }

        public Node NextNode => nextNode;

        internal Node(T d)
        {
            data = d;
            nextNode = null;
        }
    }
    public Node head = null;
    
    public void AddFront(T data)
    {
        if (head == null)
        {
            head = new Node(data);
        }
        else
        {
            Node nodeToAdd = new Node(data);
            nodeToAdd.nextNode = head;
            head = nodeToAdd;
        }
    }

    public void AddAfter(Node previousNode, T data)
    {
        if (previousNode == null)
        {
            return;
        }
        Node nodeToAdd = new Node(data);
        nodeToAdd.nextNode = previousNode.nextNode;
        previousNode.nextNode = nodeToAdd;
    }
}
