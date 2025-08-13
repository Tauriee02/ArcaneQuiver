using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public FixedJoystick joystick;
    public float moveSpeed = 2f;

    public bool useCameraBounds = true;
    private Camera mainCamera;
    private float minX, maxX, minY, maxY;
    private float playerWidth, playerHeight;

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

        if (useCameraBounds)
        {
            SetupCameraBounds();
        }
    }

    void Update()
    {
        movement = new Vector2(joystick.Horizontal, joystick.Vertical);
    }

    void FixedUpdate()
    {
        rb.velocity = movement * moveSpeed;

        if (useCameraBounds)
        {
            Vector3 clampedPosition = transform.position;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
            clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);
            transform.position = clampedPosition;
        }
    }

    void SetupCameraBounds()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("❌ Camera.main non trovata!");
            useCameraBounds = false;
            return;
        }

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            playerWidth = sr.bounds.extents.x;  
            playerHeight = sr.bounds.extents.y; 
        }
        else
        {
            playerWidth = 0.5f;
            playerHeight = 0.5f;
        }

        float cameraHalfHeight = mainCamera.orthographicSize;
        float cameraHalfWidth = mainCamera.orthographicSize * mainCamera.aspect;

        minX = -cameraHalfWidth + playerWidth;
        maxX = cameraHalfWidth - playerWidth;
        minY = -cameraHalfHeight + playerHeight;
        maxY = cameraHalfHeight - playerHeight;

        Debug.Log($"📏 Limiti player: X [{minX:F1}, {maxX:F1}], Y [{minY:F1}, {maxY:F1}]");
    }
}