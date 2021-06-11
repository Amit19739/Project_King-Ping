using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall_Script : MonoBehaviour
{
    private float cannonX;
    private float targetX;
    private float nextX;
    private float dist;
    private float baseY;
    private float height;

    public GameObject cannon;
    public GameObject target;

    public Vector3 tragetPosition;
    public Vector3 movePosition;


    public float speed;

    PlayerBasic callPlayerDamage;

    void Start()
    {
        cannon = GameObject.FindGameObjectWithTag("Cannon");
        target = GameObject.FindGameObjectWithTag("Player");

        tragetPosition = target.transform.position;

        callPlayerDamage = FindObjectOfType<PlayerBasic>();
    }

    void Update()
    {
        cannonX = cannon.transform.position.x;
        targetX = tragetPosition.x;
        dist = targetX - cannonX;
        nextX = Mathf.MoveTowards(transform.position.x, targetX, speed * Time.deltaTime);
        //nextX = Mathf.MoveTowards(transform.position.x, targetX);
        baseY = Mathf.Lerp(cannon.transform.position.y, target.transform.position.y, (nextX - cannonX) / dist);
        height = 2 * (nextX - cannonX) * (nextX - targetX) / (-0.25f * dist * dist);

        movePosition = new Vector3(nextX, baseY + height, transform.position.z);

        transform.rotation = LookAtTarget(movePosition - transform.position);
        transform.position = movePosition;

        if (movePosition == tragetPosition)
        {
            Destroy(gameObject);
        }
    }


    public static Quaternion LookAtTarget(Vector2 r)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(r.y, r.x) * Mathf.Rad2Deg);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit :" + collision.name);
            Destroy(gameObject);
            callPlayerDamage.TakeDamage(20);
        }
    }


    //void Update()
    //{
    //    cannonX = cannon.transform.position.x;
    //    targetX = target.transform.position.x;
    //    dist = targetX - cannonX;
    //    nextX = Mathf.MoveTowards(transform.position.x, targetX, speed * Time.deltaTime);
    //    baseY = Mathf.Lerp(cannon.transform.position.y, target.transform.position.y, (nextX - cannonX) / dist);
    //    height = 2 * (nextX - cannonX) * (nextX - targetX) / (-0.25f * dist * dist);

    //    movePosition = new Vector3(nextX, baseY + height, transform.position.z);

    //    transform.rotation = LookAtTarget(movePosition - transform.position);
    //    transform.position = movePosition;

    //    if (movePosition == target.transform.position)
    //    {
    //        Destroy(gameObject);
    //    }
    //}

}
