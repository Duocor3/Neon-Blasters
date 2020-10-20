using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // declare variables
    public float speed;
    public float torque;
    public Transform healthBar;

    private Rigidbody2D rb;
    private double health;

    // Use this for initialization
    void Start () {
        speed = 14f;
        torque = 4f;
        health = 100f;

        rb = GetComponent<Rigidbody2D>();
        healthBar.localScale = new Vector2(1f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");

        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis("Vertical");

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(0f, moveVertical);

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rb.AddRelativeForce(movement * speed);
        transform.Rotate(0f, 0f, -moveHorizontal * torque);
    }

    // returns the health of the player
    public double GetHealth()
    {
        return health;
    }

    // drops the health of the player
    private void DecreaseHealth(double decrease)
    {
        // decreases health as long as the health is greater than zero
        if (health-decrease > 0)
        {
            // reduces the player's health
            health -= decrease;
            // updates the health bar
            healthBar.localScale = new Vector2((float)(health / 100), 1f);
        } else
        {
            // sets the health bar to zero if the damage is greather than the player's health
            health = 0;
            healthBar.localScale = new Vector2(0f, 1f);
        }
    }

    // increases the health of the player
    private void IncreaseHealth(double increase)
    {
        if (health+increase < 100)
        {
            DecreaseHealth(increase * -1f);
        } else
        {
            health = 100;
            healthBar.localScale = new Vector2(1f, 1f);
        }
    }
}
