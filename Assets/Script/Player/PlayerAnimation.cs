using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public void Move(float move)
    {
        anim.SetFloat("Run", Mathf.Abs(move));
    }

    public void Jump(bool jumping)
    {
        anim.SetBool("Jumping", jumping);
    }

    public void Attack()
    {
        anim.SetTrigger("Attack");
    }

    public void Hit()
    {
        anim.SetTrigger("Hit");
    }

    public void Death()
    {
        anim.SetBool("Death", true);
    }

    public void OpenDoor()
    {
        anim.SetTrigger("DoorIn");
    }
}
