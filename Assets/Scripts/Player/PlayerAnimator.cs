using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator animator { get; private set; }

    

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void SetMoveAnimation(bool isMoving)
    {
        Debug.Log("SetMoveAnimation: " + isMoving);
        animator.SetBool("IsMove", isMoving);
    }

    public void TriggerIsShot()
    {
        animator.SetTrigger("IsShot");
    }

    public void TriggerIsDead()
    {
        animator.SetTrigger("IsDead");
    }
}
