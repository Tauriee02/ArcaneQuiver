using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Joystick joystick; // riferimento al joystick
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float moveX = joystick.Horizontal;
        float moveY = joystick.Vertical;

        // Imposta i parametri dell’animator
        animator.SetFloat("Horizontal", moveX);
        animator.SetFloat("Vertical", moveY);
    }
}
