using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaAnimator : MonoBehaviour
{
    private Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Jump()
    {
        animator.SetTrigger("OnJump");
    }
    
    public void StickToTheWall()
    {
        animator.SetTrigger("OnTouchTheWall");
    }
}
