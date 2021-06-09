using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon_Fire : MonoBehaviour
{
    public Transform firePoint;

    public GameObject cannonBallPrefab;

    public Animator cannonAnim;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ShootCannon();
        }
    }

    void ShootCannon()
    {
        Instantiate(cannonBallPrefab, firePoint.position, firePoint.rotation);
        cannonAnim.SetTrigger("Shoot");
    }
}
