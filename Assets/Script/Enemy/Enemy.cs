using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{

    [SerializeField] protected int health;
    [SerializeField] protected int speed;
    [SerializeField] protected int gems;

    //protected Transform pointA, pointB;
    [SerializeField] protected RaycastHit2D rightWall;
    [SerializeField] protected RaycastHit2D leftWall;
    [SerializeField] protected RaycastHit2D rightLedge;
    [SerializeField] protected RaycastHit2D leftLedge;

    public virtual void Attack()
    {

    }

    public abstract void Update();
}
