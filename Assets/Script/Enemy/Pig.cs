using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : Enemy
{
    //private Vector3 _currentTarget;
    private bool _switch;
    private Animator _anim;
    private SpriteRenderer _pigSprite;

    private void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        _pigSprite = GetComponentInChildren<SpriteRenderer>();
    }

    public override void Update()
    {
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            return;
        }

        Movement();
    }

    void Movement()
    {
        if (_switch == false)
        {
            _pigSprite.flipX = false;
        }
        else
        {
            _pigSprite.flipX = true;
        }

        //if (transform.position == pointA.position)
        //{
        //    _switch = false;
        //    _anim.SetTrigger("Idle");
        //}
        //else if (transform.position == pointB.position)
        //{
        //    _switch = true;
        //    _anim.SetTrigger("Idle");
        //}

        //if (_switch == false)
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);
        //}
        //else if (_switch == true)
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, pointA.position, speed * Time.deltaTime);
        //}
    }

    //void Movement()
    //{
    //    if (transform.position == pointA.position)
    //    {
    //        _currentTarget = pointB.position;
    //    }
    //    else if (transform.position == pointB.position)
    //    {
    //        _currentTarget = pointA.position;
    //    }
    //    transform.position = Vector3.MoveTowards(transform.position, _currentTarget, speed * Time.deltaTime);
    //}
}
