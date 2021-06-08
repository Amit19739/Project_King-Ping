using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    private Animator doorAnim;

    public SpriteRenderer spriteRendrer;

    public float distance;

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
        if (pickUpIteams.key >= 1)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                OpenDoor();
                pickUpIteams.key = 0;
                spriteRendrer.color = Color.green;
                playerScript.transform.position = transform.Find("DoorPositionFinder").position;
                playerScript.HappenWhenDoorIn();
            }
        }
        Debug.Log("Key No");
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    float dist = Vector3.Distance(transform.position, playerScript.transform.position);

    //    Debug.Log(dist);
    //    if(dist < distance)
    //    {
    //        if (pickUpIteams.key >= 1)
    //        {
    //            if (collision.gameObject.CompareTag("Player"))
    //            {
    //                OpenDoor();
    //                pickUpIteams.key = 0;
    //                spriteRendrer.color = Color.green;
    //                //playerScript.HappenWhenDoorIn();
    //            }
    //        }
    //    }
    //}

    public void OpenDoor()
    {
        doorAnim.SetTrigger("Opening Door");
    }
}