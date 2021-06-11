using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigWithMatch_Script1 : MonoBehaviour
{
    [SerializeField] private Animator pigWithMatchAnim;

    private float nextAttackTime = 0f;
    public float detectionRange;
    public float attackRate;

    public LayerMask whoIsPlayer;

    public Cannon_Fire cannon_Fire;

    private void Start()
    {
        pigWithMatchAnim = GetComponentInChildren<Animator>();
    }


    void Update()
    {
        PlayerInRange();
    }

    public void FireCannon()
    {
        pigWithMatchAnim.SetTrigger("FireCannon");
    }

    void PlayerInRange()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(transform.position, detectionRange, whoIsPlayer);

        foreach (Collider2D player in hitPlayer)
        {
            Debug.Log("Hey " + player.name);

            if (Time.time >= nextAttackTime)
            {
                FireCannon();
                cannon_Fire.ShootCannon();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRange);     
    }


}
