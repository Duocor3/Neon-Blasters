using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

    //Public variable to store a reference to the player game object
    public GameObject player;

    //Private variable to store the offset distance between the player and camera
    private Vector3 offset;            

    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.1f, 0.95f, 10));
        offset = transform.position - player.transform.position;
    }

    // LateUpdate is called after Update each frame
    void Update()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        transform.position = player.transform.position + offset;
    }
}
