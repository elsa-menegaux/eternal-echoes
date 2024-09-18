using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D body;
    public float playerSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get input from the player
        // Works with zqsd and arrows
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        // Parallel character movement with direction input, with speed factor to be able to influence gameplay further on
        Vector2 movement = new Vector2(xInput, yInput);

        body.velocity = movement * playerSpeed;
    }
}
