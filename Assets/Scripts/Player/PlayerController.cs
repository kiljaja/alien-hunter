using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public GameObject projectile;
    private Rigidbody2D rb2d;
    private float verticalMovement;
    private float horizontalMovement;
    
    void Start()
    {
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

}
  
