using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Walk(float horizontalInput, float verticalInput)
    {
        if (horizontalInput != 0 || verticalInput != 0)
        {
            animator.SetBool("isWalking", true);
        }
        else if (horizontalInput == 0 && verticalInput == 0)
        {
            animator.SetBool("isWalking", false);
        }
    }

    public void StartAnimation(string animation)
    {
        animator.SetBool(animation, true);
    }

    public void GoToIdle(string state)
    {
        animator.SetBool(state, false);
    }

    public void End(string state)
    {
        GoToIdle(state);
    }

    public Animator GetAnimator() => animator;
}
