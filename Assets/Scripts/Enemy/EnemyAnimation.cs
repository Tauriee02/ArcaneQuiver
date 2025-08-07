using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayRun(bool state)
    {
        anim.SetBool("IsRunning", state);
    }

    public void PlayAttack()
    {
        anim.SetTrigger("Attack");
    }

    public void PlayHurt()
    {
        anim.SetTrigger("Hurt");
    }

    public void PlayDeath()
    {
        anim.SetTrigger("Die");
    }
}
