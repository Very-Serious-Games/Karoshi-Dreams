using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    private Animator animator;

    public string animationName;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayAnimation()
    {
        animator.Play(animationName);
    }
}