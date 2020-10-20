using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoostController : MonoBehaviour {

    public GameObject healthBoost;

	// Use this for initialization
	void Start () {
        // makes it spin slowly
        healthBoost.GetComponent<Rigidbody2D>().angularVelocity = 100f;
    }

    // Update is called once per frame
    void Update () {

	}

    // checks object obstacle collided with
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if it collided with the player, it does damage based on the speed of the collision
        if (collision.gameObject.tag == "Player")
        {
            // increases the health of the player if it touches a health boost
            collision.gameObject.SendMessage("IncreaseHealth", 20f);
            healthBoost.SetActive(false);
        }
    }

    void ApplyDamage(double damage)
    {
        // does nothing but stop an annoying error
    }
}
