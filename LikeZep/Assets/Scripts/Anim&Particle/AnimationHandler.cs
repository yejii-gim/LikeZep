using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private static readonly int IsMoving = Animator.StringToHash("isMove");
    private static readonly int IsOpening = Animator.StringToHash("isOpen");
    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Move(Vector2 obj)
    {
        animator.SetBool(IsMoving, obj.magnitude > 0.5f);
    }

    public void Stop()
    {
        animator.SetBool(IsMoving, false);
    }

    public void Open()
    {
        animator.SetBool(IsOpening, true);
    }
}

