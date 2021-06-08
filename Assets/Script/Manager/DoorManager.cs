using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    private Animator doorAnim;

    PickUpIteams pickUpIteams;

    PlayerAnimation playerAnim;

    // Start is called before the first frame update
    void Start()
    {
        doorAnim = GetComponent<Animator>();

        pickUpIteams = FindObjectOfType<PickUpIteams>();
        playerAnim = FindObjectOfType<PlayerAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(pickUpIteams.key == 1)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("1 2 3 4 yaaaaaaaaaaaaaaaaaa!!!!!!!!!!");
                OpenDoor();
                pickUpIteams.key = 0;
                playerAnim.OpenDoor();
            }
        }
        Debug.Log("Key No");
    }

    public void OpenDoor()
    {
        doorAnim.SetTrigger("Opening Door");
    }
}