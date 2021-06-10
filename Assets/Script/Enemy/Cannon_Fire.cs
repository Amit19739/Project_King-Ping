using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon_Fire : MonoBehaviour
{
    public Transform firePoint;
    public GameObject cannonBallPrefab;
    public Animator cannonAnim;
    public float speed = 10f;

    public void ShootCannon()
    {
        //Instantiate(cannonBallPrefab, firePoint.position, firePoint.rotation);
        //cannonAnim.SetTrigger("Shoot");

        GameObject CreatedCannonball = Instantiate(cannonBallPrefab, firePoint.position, firePoint.rotation);
        CreatedCannonball.GetComponent<Rigidbody>().velocity = firePoint.transform.right * speed;
    }

    //IEnumerator WaitBeforeFireCannon()
    //{
    //    yield return new WaitForSeconds(0.4f);
    //    ShootCannon();
    //}

    //public void CannonReadyFire()
    //{
    //    StartCoroutine(WaitBeforeFireCannon());
    //}
}
