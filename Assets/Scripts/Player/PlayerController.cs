using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] 
    private float speed = 2;
    public GameObject projectile;
    private Rigidbody2D rb2d;
    private Animator anim;
    private float verticalMovement;
    private float horizontalMovement;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");

        if(Input.GetButton("Fire1")){
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

    void Shoot(){
        float yOffset = 1f;
        Vector2 spawnLocation = new Vector2(this.transform.position.x, this.transform.position.y + yOffset);
        GameObject newBullet = Instantiate(projectile, spawnLocation, Quaternion.identity) as GameObject;
    }

    private void Die(){
        anim.SetTrigger("Die");
        Destroy(gameObject, .750f);
    }

}
  
