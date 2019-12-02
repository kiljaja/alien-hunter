using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    [SerializeField] private int health = 1;
    [SerializeField] private int damage = 20;
    [SerializeField] private float speed = 5;
    [SerializeField] private float rotateSpeed = 200;
    [SerializeField]
    private Transform target;
    private AudioSource burstSound;
    private Animator anim;
    private Rigidbody2D rb2d;

    private void Awake()
    {
        if (rb2d == null) rb2d = GetComponent<Rigidbody2D>();
        if (anim == null) anim = GetComponent<Animator>();
        if (burstSound == null) burstSound = GetComponent<AudioSource>();
    }

    private void Start(){
        PlayBurstSound();
    }
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
            col.gameObject.GetComponent<ShotController>().SelfDestroy();
        }
        else if (col.gameObject.CompareTag("Player"))
        {
            Die();
        }
    }
    private void HandleMovement()
    {
        if (target == null) return; //stop if no target
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
        PlayBurstSound();
        anim.SetTrigger("Die");
        Destroy(gameObject, .35f);
    }

    public int GetDamage()
    {
        return (health > 0) ? this.damage : 0;
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

    private void PlayBurstSound(){ 
        AudioSource.PlayClipAtPoint(burstSound.clip, this.gameObject.transform.position, PlayerPrefs.GetFloat("fxVolume"));
    }

}
