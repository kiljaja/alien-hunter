using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb2d;
    private Animator anim;
    private int damage;
    void Awake()
    {
        if(rb2d == null) rb2d = GetComponent<Rigidbody2D>();
        if(anim == null) anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Boundary") Destroy(gameObject, 1);
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

    public void SetDamage(int damage){
        if(damage < 0) damage = 0;
        this.damage = damage;
    }

    public void Init( Vector2 direction, int damage){
        SetDirection(direction);
        SetDamage(damage);
    }
}
