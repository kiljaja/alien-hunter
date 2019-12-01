using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    [SerializeField] private int health = 2;
    [SerializeField] private int damage = 20;
    [SerializeField] private float speed = 5;
    [SerializeField] private float rotateSpeed = 200;
    [SerializeField]
    private Transform target;
    private Animator anim;
    private Rigidbody2D rb2d;

    private void Awake()
    {
        if (rb2d == null) rb2d = GetComponent<Rigidbody2D>();
        if (anim == null) anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleMovement();

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("PlayerShot") && health > 0)
        {
            int damage = 1;
            TakeDamage(damage);
        }
        else if (col.gameObject.CompareTag("Player"))
        {
            Die();
        }
    }



    private void HandleMovement()
    {
        Vector2 direction = (Vector2)target.position - rb2d.position;
        direction.Normalize();
        float roateAmount = Vector3.Cross(direction, -transform.up).z;
        rb2d.angularVelocity = -roateAmount * rotateSpeed;
        rb2d.velocity = -transform.up * speed;
    }

    private void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            Die();
        }
    }

    void Die()
    {
        health = 0;
        anim.SetTrigger("Die");
        Destroy(gameObject, .35f);
    }

    public int GetDamage()
    {
        return this.damage;
    }

    public void SetDamage(int damage)
    {
        if (damage < 0) damage = 0;
        this.damage = damage;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void Init(Transform target, int damage, float speed)
    {
        SetTarget(target);
        SetDamage(damage);
        SetSpeed(speed);
    }

}
