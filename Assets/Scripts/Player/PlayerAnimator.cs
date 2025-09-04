using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator animator { get; private set; }

    bool isDead = false;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        isDead = false; 
    }

    public void SetMoveAnimation(bool isMoving)
    {
        if(isDead) return;

        Debug.Log("SetMoveAnimation: " + isMoving);
        animator.SetBool("IsMove", isMoving);
    }

    public void TriggerIsShot()
    {
        if (isDead) return;

        animator.SetTrigger("IsShot");
    }

    public void TriggerIsDead()
    {
        animator.SetTrigger("IsDead");
        isDead = true;
    }
}
