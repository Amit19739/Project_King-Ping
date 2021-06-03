using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpIteams : MonoBehaviour
{
    private int totalDiamond;

    public int recoverHealth;

    public Text diamondCountText;

    public HealthBar healthBar;

    PlayerBasic playerBasic;

    public void OnTriggerEnter2D(Collider2D pickUpIteams)
    {
        if (pickUpIteams.gameObject.CompareTag("PickUp"))
        {
            Debug.Log("Pickup");
            totalDiamond += 1;
            diamondCountText.text = totalDiamond.ToString();
            Destroy(pickUpIteams.gameObject);
        }

        //if (pickUpIteams.gameObject.CompareTag("HealthPickUp"))
        //{
        //    Debug.Log("Hurrayyyy!!!");
        //    playerBasic.currentHealth++;
        //    playerBasic.currentHealth = playerBasic.currentHealth + recoverHealth;
        //    healthBar.SetHealth(playerBasic.currentHealth);
        //    Destroy(pickUpIteams.gameObject);
        //}
    }

}
