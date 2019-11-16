using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalScroller : MonoBehaviour
{
    private Rigidbody2D rb2d;
    [SerializeField] private float speed = 1.5f;
    private float sizeOfY;
    private Renderer rend;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rend = GetComponent<Renderer>();
        sizeOfY = rend.bounds.size.y;
        rb2d.velocity = Vector2.down * speed;
    }

    void Update()
    {
        if(transform.position.y < -sizeOfY){
            RepeatBackground();
        }  
    }

    void RepeatBackground(){
        Vector2 offset = new Vector2(0, sizeOfY * 2f);
        transform.position = (Vector2) transform.position + offset;
    }
}
