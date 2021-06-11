using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorManager : MonoBehaviour
{
    public float distance;

    private Animator doorAnim;

    public SpriteRenderer spriteRendrer;

    PickUpIteams pickUpIteams;
    PlayerBasic playerScript;

    // Start is called before the first frame update
    void Start()
    {
        doorAnim = GetComponent<Animator>();

        pickUpIteams = FindObjectOfType<PickUpIteams>();
        playerScript = FindObjectOfType<PlayerBasic>();
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

        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Level_1");
            Debug.Log("Next Level");
        }
    }

    public void OpenDoor()
    {
        doorAnim.SetTrigger("Opening Door");
    }

    //IEnumerator IsPlayerInDoor()
    //{
    //    isPlayerInDoor = true;
    //    yield return new WaitForSeconds(1.2f);
    //    GetComponent<SpriteRenderer>().sortingOrder = SortingLayer.layers
    //    isPlayerInDoor = false;
    //}



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
}