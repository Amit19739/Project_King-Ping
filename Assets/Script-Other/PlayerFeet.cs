using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFeet : MonoBehaviour
{
    PlayerBasic playerBacisFeet;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        playerBacisFeet = FindObjectOfType<PlayerBasic>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") && playerBacisFeet.isGrounded())
        {
            playerBacisFeet.isGrounded();
            player.transform.parent = collision.gameObject.transform;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            player.transform.parent = null;
        }
    }
}
