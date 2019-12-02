using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private int health = 100;
    [SerializeField]
    private int MAX_HEALTH = 100;
    [SerializeField]
    private int shotDamage = 10;
    [SerializeField]
    private float shotSpeed = 5;
    [SerializeField]
    [Range(0f, 1f)]
    private float shotProbality = 0.5f;
    [SerializeField]
    private float fireRate = 1;
    [SerializeField]
    private int missileDamage = 20;
    [SerializeField]
    private float missileSpeed = 5;
    [SerializeField]
    private float missileRate = 2;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private Color HEALTH_BAR_COLOR = new Color(251, 42, 42, 255);
    public GameObject shot;
    public GameObject missile;
    private Rigidbody2D rb2d;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private HealthBarController healthBar;
    private AudioSource explosiontSound;
    private float verticalMovement;
    private float horizontalMovement;

    private void Awake()
    {
        if (anim == null) anim = GetComponent<Animator>();
        if (rb2d == null) rb2d = GetComponent<Rigidbody2D>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        if (healthBar == null) healthBar = GetComponentInChildren<HealthBarController>();
        if (explosiontSound == null) explosiontSound = GetComponent<AudioSource>();
    }

    void Start()
    {
        if (target == null) target = GameObject.FindWithTag("Player").transform;
        if(gameController == null) gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        
        verticalMovement = 0;
        horizontalMovement = 1;
        healthBar.SetHealthColor(HEALTH_BAR_COLOR);
        UpdateHealthBar();
        InvokeRepeating("ShootShot", .5f, fireRate);
        InvokeRepeating("ShootMissile", .8f, missileRate);
    }

    void Update()
    {
        
        //CheatCode but you lose points
        if (Input.GetKeyDown(KeyCode.Q)){
            gameController.DecreaseScore(100);
            Die();
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
        if (col.gameObject.CompareTag("PlayerShot") && health > 0)
        {
            int damage = col.gameObject.GetComponent<ShotController>().GetDamage();
            TakeDamage(damage);
            gameController.IncreaseScore(20);
        }

    }
    void HandleMovement()
    {
        Vector2 newVelocity = new Vector2(horizontalMovement, verticalMovement);
        rb2d.velocity = newVelocity.normalized * speed;
    }

    void Die()
    {
        PlayExplosionSound();
        anim.SetTrigger("Die");
        Destroy(gameObject, .755f);
        
    }

    private void PlayExplosionSound(){ 
        AudioSource.PlayClipAtPoint(explosiontSound.clip, this.gameObject.transform.position, PlayerPrefs.GetFloat("fxVolume"));
    }

    private bool WillShoot()
    {
        return Random.Range(0.0f, 1.0f) < shotProbality;
    }
    private void ShootShot()
    {
        if (WillShoot())
        {
            float yOffset = ((spriteRenderer.bounds.size.y / 2) + .19f) * -1;
            Vector2 spawnLocation = new Vector2(this.transform.position.x, this.transform.position.y + yOffset);
            GameObject newBullet = Instantiate(shot, spawnLocation, Quaternion.identity) as GameObject;
            newBullet.GetComponent<ShotController>().Init(Vector2.down, shotSpeed, shotDamage);
        }

    }

    private void ShootMissile()
    {
        if (WillShoot())
        {
            float yOffset = ((spriteRenderer.bounds.size.y / 2)) * -1;
            Vector2 spawnLocation = new Vector2(this.transform.position.x, this.transform.position.y);
            GameObject newMissile = Instantiate(missile, spawnLocation, Quaternion.identity) as GameObject;
            newMissile.GetComponent<MissileController>().Init(target, missileDamage, missileSpeed);
        }

    }



    private void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            Die();
        }
        UpdateHealthBar();
    }

    
    private void UpdateHealthBar()
    {
        float healthPercent = health / (float)MAX_HEALTH;
        healthBar.SetHealthPercent(healthPercent);
    }
}
