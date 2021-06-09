using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigWithMatch_Script : MonoBehaviour
{
    private float nextAttackTime = 0f;

    public float detectionRange;
    public float attackRate;

    public LayerMask whoIsPlayer;
    public Animator pigwithatchAnim;

    public Cannon_Fire cannon_Fire;


    void Update()
    {
        PlayerInRange();
    }

    public void FireCannon()
    {
        pigwithatchAnim.SetTrigger("FireCannon");
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
                cannon_Fire.CannonReadyFire();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRange);     
    }


}
