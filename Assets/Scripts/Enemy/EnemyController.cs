using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public int health;
    public float fireRate;
    public GameObject projectile;
    private Rigidbody2D rb2d;
    private float verticalMovement;
    private float horizontalMovement;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        verticalMovement = 0;
        horizontalMovement = 1;
    }

    void Update()
    {
        if (WillShoot())
        {
            Shoot();
        }

    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Boundary")
        {
            horizontalMovement *= -1;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("PlayerShot"))
        {
            // int damage = col.gameObject.GetComponent<PlayerProjectileController>();
            
            // SelfDestroy();
            Debug.Log("Enemy hit");
            
        }

    }
    void HandleMovement()
    {
        Vector2 newVelocity = new Vector2(horizontalMovement, verticalMovement);
        rb2d.velocity = newVelocity.normalized * speed;
    }

    void SelfDestroy()
    {
        Destroy(gameObject);
    }

    bool WillShoot()
    {
        return Random.Range(0.0f, 1.0f) < fireRate;
    }
    void Shoot()
    {
        float yOffset = -.5f;
        Vector2 spawnLocation = new Vector2(this.transform.position.x, this.transform.position.y + yOffset);
        Vector3 bulletRotation = new Vector3(0, 0, 180);
        GameObject newBullet = Instantiate(projectile, spawnLocation, Quaternion.Euler(bulletRotation)) as GameObject;
    }
}
