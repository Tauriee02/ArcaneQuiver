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
        if (joystick == null)
        {
            joystick = FindObjectOfType<FixedJoystick>();
        }
         if (joystick == null)
        {
            Debug.LogError("❌ FixedJoystick non trovato nella scena!");
        }
    }

    void Update()
    {
        movement = new Vector2(joystick.Horizontal, joystick.Vertical);
    }

    void FixedUpdate()
    {
        rb.velocity = movement * moveSpeed;
    }
}
