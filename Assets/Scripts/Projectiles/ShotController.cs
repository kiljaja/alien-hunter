using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    private float speed = 2;
    private Rigidbody2D rb2d;
    private Animator anim;
    private int damage = 1;
    void Awake()
    {
        if (rb2d == null) rb2d = GetComponent<Rigidbody2D>();
        if (anim == null) anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Boundary") Destroy(gameObject, 1);
        else SelfDestroy();
    }
    void SelfDestroy()
    {
        anim.SetTrigger("Die");
        Destroy(gameObject, 0.2f);
    }

    public void SetDirection(Vector2 direction)
    {
        rb2d.velocity = direction.normalized * speed;
    }

    public void SetSpeed(float speed){
        this.speed = speed;
    }

    public void SetDamage(int damage)
    {
        if (damage < 0) damage = 0;
        this.damage = damage;
    }

    public void Init(Vector2 direction, float speed, int damage)
    {
        SetSpeed(speed);
        SetDirection(direction);
        SetDamage(damage);
    }

}
