using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 2;
    [SerializeField]
    float fireRate = 1;
    [SerializeField]
    private int shotDamage = 10;
    [SerializeField]
    private float shotSpeed = 5;
    private int health = 200;
    private int MAX_HEALTH = 200;
    private Color HEALTH_BAR_COLOR = new Color(0, 255, 0, 255);
    public GameObject projectile;
    private GameController gameController;
    private Rigidbody2D rb2d;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private HealthBarController healthBar;
    private AudioSource explosiontSound;
    private float lastShot;

    private float verticalMovement;
    private float horizontalMovement;

    private void Awake()
    {
        if (anim == null) anim = GetComponent<Animator>();
        if (rb2d == null) rb2d = GetComponent<Rigidbody2D>();
        if (healthBar == null) healthBar = GetComponentInChildren<HealthBarController>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        if (explosiontSound == null) explosiontSound = GetComponent<AudioSource>();

    }
    void Start()
    {
        if (gameController == null) gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        health = PlayerPrefs.GetInt("playerHealth");
        healthBar.SetHealthColor(HEALTH_BAR_COLOR);
        UpdateHealthBar();
    }

    void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");
        if (CanShoot() && Input.GetButton("Fire1"))
        {
            Shoot();
        }

    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        Vector2 newVelocity = new Vector2(horizontalMovement, verticalMovement);
        rb2d.velocity = newVelocity.normalized * speed;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Die();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("EnemyShot") && health > 0)
        {
            int damage = col.gameObject.GetComponent<ShotController>().GetDamage();
            TakeDamage(damage);
            gameController.DecreaseScore(10);
        }

        if (col.gameObject.CompareTag("EnemyMissile") && health > 0)
        {
            int damage = col.gameObject.GetComponent<MissileController>().GetDamage();
            TakeDamage(damage);
            gameController.DecreaseScore(10);
        }

    }

    void Shoot()
    {
        lastShot = Time.time;
        float yOffset = (spriteRenderer.bounds.size.y / 2) + .24f;
        Vector2 spawnLocation = new Vector2(this.transform.position.x, this.transform.position.y + yOffset);
        GameObject newBullet = Instantiate(projectile, spawnLocation, Quaternion.identity) as GameObject;
        newBullet.GetComponent<ShotController>().Init(Vector2.up, shotSpeed, shotDamage);
    }

    private void Die()
    {
        PlayExplosionSound();
        anim.SetTrigger("Die");
        gameController.DecreaseScore(30);
        PlayerPrefs.SetInt("playerHealth", 200);
        Destroy(gameObject, .750f);
    }

    private void PlayExplosionSound()
    {
        AudioSource.PlayClipAtPoint(explosiontSound.clip, this.gameObject.transform.position, PlayerPrefs.GetFloat("fxVolume"));
    }

    private void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0) health = 0;
        PlayerPrefs.SetInt("playerHealth", health);
        if (health == 0)
        {
            Die();
        }

        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        float healthPercent = health / (float)MAX_HEALTH;
        healthBar.SetHealthPercent(healthPercent);
    }

    private bool CanShoot()
    {
        return Time.time > (lastShot + fireRate);
    }

}

