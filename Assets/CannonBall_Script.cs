using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall_Script : MonoBehaviour
{
    public float speed = 20f;

    public Rigidbody2D rb;

    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        Destroy(gameObject);
    }
}
