using UnityEngine;

public class NinjaAnimator : MonoBehaviour
{
    private const string ON_JUMP_ANIMATION_TRIGGER_NAME = "OnJump";
    private const string ON_TOUCH_THE_WALL_ANIMATION_TRIGGER_NAME = "OnTouchTheWall";
    
    private Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerJumpAnimation()
    {
        animator.SetTrigger(ON_JUMP_ANIMATION_TRIGGER_NAME);
    }
    
    public void TriggerTouchTheWallAnimation()
    {
        animator.SetTrigger(ON_TOUCH_THE_WALL_ANIMATION_TRIGGER_NAME);
    }
}
