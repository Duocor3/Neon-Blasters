using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    public GameObject obstacle;
    public GameObject perk;
    public GameObject gameController;
    public Rigidbody2D rb;

    public double health;
    public bool isDamageable;

	// Use this for initialization
	void Start () {
        health = 100f;

        if (isDamageable)
        {
            // applies a random rotation and force to the obstacle
            rb.AddTorque(Random.Range(-100, 100));
            rb.AddRelativeForce(new Vector2(Random.Range(-300, 300), Random.Range(-300, 300)));
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    // reduces the health of the obstacle
    public void ApplyDamage(double damage)
    {
        // only applies damage if it's a damageable object
        if (isDamageable)
        {
            health -= damage/10;
            Debug.Log(health);
            // breaks the object if the health less than or equal to zero
            if (health <= 0)
            {
                health = 0;
                // spawns a new random obstacle somewhere else on the map
                Instantiate(obstacle, new Vector2(Random.Range(-100, 100),
                    Random.Range(-100, 100)), Quaternion.identity);
                // has a 1 in 2 chance of spawning a perk after its destruction
                if (Random.Range(0, 100) >= 50)
                {
                    Instantiate(perk, transform.position, Quaternion.identity);
                }
                // adds points to the score
                gameController.SendMessage("AddPoints", 100);
                // hides the obstacle
                obstacle.SetActive(false);
            }
        }
    }

    // checks object obstacle collided with
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if it collided with the player, it does damage based on the speed of the collision
        if (collision.gameObject.tag == "Player" && isDamageable)
        {
            collision.gameObject.SendMessage("DecreaseHealth",
                rb.velocity.magnitude * collision.rigidbody.velocity.magnitude * 0.3f);
        }
    }
}
