using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    private Animator doorAnim;

    public SpriteRenderer spriteRendrer;

    PickUpIteams pickUpIteams;
    PlayerBasic playerScript;

    // Start is called before the first frame update
    void Start()
    {
        doorAnim = GetComponent<Animator>();

        //spriteRendrer = GetComponentInChildren<SpriteRenderer>();

        pickUpIteams = FindObjectOfType<PickUpIteams>();
        playerScript = FindObjectOfType<PlayerBasic>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(pickUpIteams.key >= 1)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                OpenDoor();
                pickUpIteams.key = 0;
                spriteRendrer.color = Color.green;
                playerScript.HappenWhenDoorIn();
            }
        }
        Debug.Log("Key No");
    }

    public void OpenDoor()
    {
        doorAnim.SetTrigger("Opening Door");
    }
}