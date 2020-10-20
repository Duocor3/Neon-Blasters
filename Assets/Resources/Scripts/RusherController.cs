using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RusherController : MonoBehaviour {

    public GameObject gameController;
    public Rigidbody2D rusher;
    public Rigidbody2D player;

    private double health;

	// Use this for initialization
	void Start () {
        health = 200f;

        InvokeRepeating("ChangeDirection", 0f, 1f);
    }

    // Update is called once per frame
    void Update () {

    }

    // changes directions randomly
    public void ChangeDirection()
    {
        // makes sure the object is active in the first place
        if (rusher.gameObject.activeSelf)
        // changes movement pattern based on distance to player
        if (CheckCloseToTag("Player", 10f))
        {
            rusher.angularVelocity = 600f;
            StartCoroutine(Dash());
        }
        else
        {
            rusher.angularVelocity = 200f;
            StartCoroutine(WaitChange(0.5f));
        }
    }

    // adds the delay
    IEnumerator WaitChange(float seconds)
    {
        rusher.velocity = Vector2.zero;
        yield return new WaitForSeconds(seconds);
        rusher.velocity = new Vector2(Random.Range(-8, 8), Random.Range(-8, 8));
    }

    // dashes towards the player
    IEnumerator Dash()
    {
        rusher.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.3f);
        rusher.velocity = (player.position - rusher.position).normalized * 10f;
    }

    // checks object obstacle collided with
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if it collided with the player, it does damage based on the speed of the collision
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.SendMessage("DecreaseHealth",
                rusher.velocity.magnitude * collision.rigidbody.velocity.magnitude * 0.3f);
        }
    }

    // reduces the health of the obstacle
    public void ApplyDamage(double damage)
    {
        health -= damage / 10;
        Debug.Log(health);

        // breaks the object if the health less than or equal to zero
        if (health <= 0)
        {
            health = 0;
            Instantiate(rusher, new Vector2(Random.Range(-100, 100),
                Random.Range(-100, 100)), Quaternion.identity);
            // adds points to the score
            gameController.SendMessage("AddPoints", 500);
            rusher.gameObject.SetActive(false);
        }
    }

    // checks if the player is in a certain radius - copied from unity forums
    bool CheckCloseToTag(string tag, float minimumDistance)
    {
        GameObject[] goWithTag = GameObject.FindGameObjectsWithTag(tag);

        for (int i = 0; i < goWithTag.Length; i++)
        {
            if (Vector3.Distance(transform.position, goWithTag[i].transform.position) <= minimumDistance)
                return true;
        }

        return false;
    }
}
