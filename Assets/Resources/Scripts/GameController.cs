using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject obstaclePrefab;
    public GameObject rusherPrefab;
    public GameObject player;

    public Text gameOver;
    public Text pointsText;

    private int numObstacles;
    private int numRushers;
    private int points;

	// Use this for initialization
	void Start () {
        // sets the max number of meteorite obstacles at any point in the game
        numObstacles = 50;
        numRushers = 50;
        points = 0;

        gameOver.text = "";

        for (int i = 0; i < numObstacles; i++)
        {
            // randomly spawns the number of obstacles required randomly in the game arena
            Instantiate(obstaclePrefab, new Vector2(Random.Range(-100, 100),
                Random.Range(-100, 100)), Quaternion.identity);
        }

        for (int i = 0; i < numRushers; i++)
        {
            // randomly spawns the number of rushers required randomly in the game arena
            Instantiate(rusherPrefab, new Vector2(Random.Range(-100, 100),
                Random.Range(-100, 100)), Quaternion.identity);
        }
    }
	
	// Update is called once per frame
	void Update () {
		// checks if the player is dead
        if (player.GetComponent<Player>().GetHealth() <= 0)
        {
            // hides the player
            player.GetComponent<Player>().healthBar.localScale = new Vector2(0f, 1f);
            player.SetActive(false);
            // displays the game over text
            gameOver.text = "Game Over!\nPress R to Restart";

            // restarts the game if the player pressed R and the game over screen is showing
            if (Input.GetKey(KeyCode.R)) {
                SceneManager.LoadScene("Play Scene");
            }
        }
	}

    // adds points to the player's score
    public void AddPoints(int amount)
    {
        points += amount;
        pointsText.text = "Points: " + points;
    }
}
