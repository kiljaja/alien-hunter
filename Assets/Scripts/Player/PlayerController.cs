using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] 
    private float speed = 2;
    private int health = 100;
    private int MAX_HEALTH = 100;
    private Color HEALTH_BAR_COLOR = new Color(0, 255, 0, 255);
    public GameObject projectile;
    private Rigidbody2D rb2d;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private HealthBarController healthBar;
    private float verticalMovement;
    private float horizontalMovement;
    
    void Start()
    {
        health = GetHealth();
        if(anim == null) anim = GetComponent<Animator>();
        if(rb2d == null ) rb2d = GetComponent<Rigidbody2D>();
        if(healthBar == null) healthBar = GetComponentInChildren<HealthBarController>();
        if(spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        healthBar.SetHealthColor(HEALTH_BAR_COLOR);
        UpdateHealthBar();
    }

    void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");
        if(Input.GetButton("Fire1")){
            Shoot();
        }

        //For testing 
        if(Input.GetKeyDown(KeyCode.P)){
            TakeDamage(Random.Range(20, 30));
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

    void Shoot(){
        float yOffset = (spriteRenderer.bounds.size.y/ 2) + .5f;
        Vector2 spawnLocation = new Vector2(this.transform.position.x, this.transform.position.y + yOffset);
        GameObject newBullet = Instantiate(projectile, spawnLocation, Quaternion.identity) as GameObject;
    }

    private void Die(){
        anim.SetTrigger("Die");
        Destroy(gameObject, .750f);
    }

    private int GetHealth(){
        int savedHealth = 100;
        return (savedHealth < health)? savedHealth : health;
    }

    private void TakeDamage(int damage){
        health -= damage;
        if(health <= 0){
            health = 0;
            Die();
        }
        UpdateHealthBar();
    }

    private void UpdateHealthBar(){
        float healthPercent = health / (float)MAX_HEALTH;
        healthBar.SetHealthPercent(healthPercent);
    }

}
  
