using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpIteams : MonoBehaviour
{
    private int totalDiamond;

    public int key;

    public Text diamondCountText;

    public HealthBar healthBar;

    PlayerBasic playerBasic;            //player script

    private void Awake()
    {
        playerBasic = FindObjectOfType<PlayerBasic>();      //player script reference
    }

    public void OnTriggerEnter2D(Collider2D pickUpIteams)
    {
        if (pickUpIteams.gameObject.CompareTag("PickUp"))
        {
            Debug.Log("Pickup");
            totalDiamond += 1;
            diamondCountText.text = totalDiamond.ToString();
            Destroy(pickUpIteams.gameObject);
        }

        if (pickUpIteams.gameObject.CompareTag("Key"))
        {
            Debug.Log("Key is here!!!!!!");
            key += 1;
            Destroy(pickUpIteams.gameObject);
        }


        //Regain Health when player heleth is lower than the current healt
        if (pickUpIteams.gameObject.CompareTag("HealthPickUp"))
        {
            if (playerBasic.currentHealth < playerBasic.maxHealth)
            {
                playerBasic.currentHealth += 20;  //Recover 20 point when player hit this
                healthBar.SetHealth(playerBasic.currentHealth);
                Destroy(pickUpIteams.gameObject);
            }
        }

        if (pickUpIteams.gameObject.CompareTag("SmallHeart"))
        {
            if (playerBasic.currentHealth < playerBasic.maxHealth)
            {
                playerBasic.currentHealth += 10;    //Recover 10 point when player hit this
                healthBar.SetHealth(playerBasic.currentHealth);
                Destroy(pickUpIteams.gameObject);
            }
        }
    }

}
