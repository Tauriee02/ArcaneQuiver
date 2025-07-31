using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public FixedJoystick joystick;
    public float moveSpeed = 2f;

    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement = new Vector2(joystick.Horizontal, joystick.Vertical);
        Debug.Log("Joystick input: " + input);
    }

    void FixedUpdate()
    {
        rb.velocity = movement * moveSpeed;
    }
}
