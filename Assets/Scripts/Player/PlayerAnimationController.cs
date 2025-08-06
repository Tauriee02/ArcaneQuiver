using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public FixedJoystick joystick; 
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public float deadZone = 0.2f;
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        joystick = FindObjectOfType<FixedJoystick>();
        
        if (joystick == null)
        {
            Debug.LogError("❌ FixedJoystick non trovato nella scena! L'animazione non funzionerà.");
        }
        else
        {
            Debug.Log("✅ Joystick trovato: " + joystick.name);
        }
    }

    void Update()
    {
        if (joystick == null || animator == null)
        {
            return; 
        }
        float moveX = joystick.Horizontal;
        float moveY = joystick.Vertical;

        
        bool isMoving = Mathf.Abs(moveX) > deadZone || Mathf.Abs(moveY) > deadZone;

        // Imposta i parametri dell’animator
        animator.SetFloat("Horizontal", moveX);
        animator.SetFloat("Vertical", moveY);
        animator.SetBool("IsMoving", isMoving);
    }
}
