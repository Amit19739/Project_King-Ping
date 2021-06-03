using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigBase1 : MonoBehaviour
{
    [SerializeField] private float pigSpeed;

    private bool facingRight = true;

    private RaycastHit2D leftWall;
    private RaycastHit2D rightWall;

    public Transform colliderDetection;
    public LayerMask groundLayer;

    //private Vector3 direction;

    void Start()
    {

    }

    void Update()
    {
        //direction = new Vector3(0, -180, 0);
        //transform.eulerAngles = direction;

        PigMovement(); 
    }

    void PigMovement()
    {
        //Player move left
        transform.Translate(Vector2.right * pigSpeed * Time.deltaTime);

        //Check for ledges
        RaycastHit2D groundInfo = Physics2D.Raycast(colliderDetection.position, Vector2.down, 1f, groundLayer.value);
        Debug.DrawRay(colliderDetection.position, Vector2.down, Color.red);
        if (groundInfo.collider == null)
        {
            if (facingRight == true)
            {
                Flip();
                pigSpeed *= -1;
                facingRight = false;
            }
            else
            {
                Flip();
                pigSpeed *= -1;
                facingRight = true;
            }
        }

        //check for walls
        if (groundInfo.collider != null && facingRight == true)
        {
            leftWall = Physics2D.Raycast(new Vector2(colliderDetection.position.x, colliderDetection.position.y), Vector2.left, 0.5f);
            Debug.DrawRay(colliderDetection.position, Vector2.left, Color.red);
            if (leftWall.collider != null)
            {
                Flip();
                //pigSpeed *= -1;
                facingRight = false;
            }
        }
        else
        {
            rightWall = Physics2D.Raycast(new Vector2(colliderDetection.position.x, colliderDetection.position.y), Vector2.right, 0.5f);
            Debug.DrawRay(colliderDetection.position, Vector2.right, Color.red);
            if (rightWall.collider != null)
            {
                Flip();
                //pigSpeed *= -1;
                facingRight = true;
            }
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
       facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}