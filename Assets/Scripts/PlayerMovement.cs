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
        // Get the correct rigid body (and check it has been found) 
        body = GetComponent<Rigidbody2D>();
        if (body == null)
        {
            Debug.LogError("Rigidbody2D component not found on player.");
        }
    }

    // Update is called once per frame
    // Contains a few debug logs because of issues with this script
    void Update()
    {
        // Get input from the player
        // Works with zqsd and arrows
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");
        // Debug.Log($"xInput: {xInput}, yInput: {yInput}");

        // Parallel character movement with direction input, with speed factor to be able to influence gameplay further on
        Vector2 movement = new Vector2(xInput, yInput);
        // Debug.Log($"Movement Vector: {movement}");

        body.velocity = movement * playerSpeed;
        // Debug.Log($"Velocity : {body.velocity}");
    }
}
