using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb2d;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        SetDirection(Vector2.up);
    }

    void Update()
    {

    }



    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer != 13)
        {
            SelfDestroy();
        }

    }
    void SelfDestroy()
    {
        Destroy(gameObject);
    }

    public void SetDirection(Vector2 direction)
    {
        rb2d.velocity = direction.normalized * speed;
    }
}
