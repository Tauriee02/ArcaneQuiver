using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Joystick joystick; 
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public float deadZone = 0.2f; 
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float moveX = joystick.Horizontal;
        float moveY = joystick.Vertical;

        
        bool isMoving = Mathf.Abs(moveX) > deadZone || Mathf.Abs(moveY) > deadZone;

        // Imposta i parametri dell’animator
        animator.SetFloat("Horizontal", moveX);
        animator.SetFloat("Vertical", moveY);
        animator.SetBool("IsMoving", isMoving);
    }
}
