using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon_Fire : MonoBehaviour
{
    public Transform firePoint;
    public GameObject cannonBallPrefab;
    public Animator cannonAnim;
    public float speed;


    public void ShootCannon()
    {
        GameObject newCannonball = Instantiate(cannonBallPrefab, firePoint.position, firePoint.rotation);
        newCannonball.GetComponent<Rigidbody2D>().velocity = transform.right * speed;
        cannonAnim.SetTrigger("Shoot");
    }
}
