using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    [SerializeField] private Sprite bodySprite;
    [SerializeField] private Sprite tailSprite;

    private SpriteRenderer spriteRenderer;
   
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = tailSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
